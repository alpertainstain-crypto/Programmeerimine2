using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.VisiteDocument;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.VisiteDocument
{
    public class SaveVisitDocumentCommandHandlerTests : ServiceTestBase
    {
        private readonly SaveVisitDocumentCommandHandler _handler;

        public SaveVisitDocumentCommandHandlerTests()
        {
            _handler = new SaveVisitDocumentCommandHandler(DbContext);
        }

        [Fact]
        public async Task SaveVisitDocumentCommandHandler_WithNewVisitDocument_ShouldCreateVisitDocument()
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                Id = 0,
                AppointmentId = 1,
                FilePath = "/files/document1.pdf",
                FileType = "PDF",
                UploadedBy = 1,
                CreatedAt = DateTime.Now
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task SaveVisitDocumentCommandHandler_WithNewVisitDocument_ShouldSaveToDatabase()
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                Id = 0,
                AppointmentId = 1,
                FilePath = "/files/document2.pdf",
                FileType = "PDF",
                UploadedBy = 1,
                CreatedAt = DateTime.Now
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var documentsCount = DbContext.VisitDocuments.Count();

            // Assert
            Assert.False(result.HasErrors);
            Assert.True(documentsCount > 0);
        }

        [Fact]
        public async Task SaveVisitDocumentCommandHandler_WithExistingVisitDocument_ShouldUpdateVisitDocument()
        {
            // Arrange
            var visitDocument = new VisitDocument
            {
                AppointmentId = 1,
                FileType = "PDF",
                UploadedBy = 1,
                CreatedAt = DateTime.Now
            };

            await DbContext.VisitDocuments.AddAsync(visitDocument);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveVisitDocumentCommand
            {
                Id = visitDocument.Id,
                AppointmentId = 2,
                FilePath = "/files/updated.docx",
                FileType = "DOCX",
                UploadedBy = 2,
                CreatedAt = DateTime.Now
            };

            // Act
            var result = await _handler.Handle(updateCommand, CancellationToken.None);
            var updatedDocument = await DbContext.VisitDocuments.FindAsync(visitDocument.Id);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(updatedDocument);
            Assert.Equal(2, updatedDocument.AppointmentId);
            Assert.Equal("DOCX", updatedDocument.FileType);
            Assert.Equal(2, updatedDocument.UploadedBy);
        }

        [Fact]
        public async Task SaveVisitDocumentCommandHandler_WithNonExistentVisitDocumentId_ShouldReturnError()
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                Id = 9999,
                AppointmentId = 1,
                FilePath = "/files/nonexistent.pdf",
                FileType = "PDF",
                UploadedBy = 1,
                CreatedAt = DateTime.Now
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.HasErrors);
            Assert.Contains("Visit document not found", result.Errors);
        }

        [Fact]
        public async Task SaveVisitDocumentCommandHandler_UpdateVisitDocumentProperties_ShouldPersistAllProperties()
        {
            // Arrange
            var createdAtOriginal = new DateTime(2025, 1, 1, 10, 0, 0);
            var createdAtUpdated = new DateTime(2025, 1, 15, 14, 30, 0);

            var visitDocument = new VisitDocument
            {
                AppointmentId = 1,
                FileType = "PDF",
                UploadedBy = 1,
                CreatedAt = createdAtOriginal
            };

            await DbContext.VisitDocuments.AddAsync(visitDocument);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveVisitDocumentCommand
            {
                Id = visitDocument.Id,
                AppointmentId = 5,
                FilePath = "/files/report.xlsx",
                FileType = "XLSX",
                UploadedBy = 3,
                CreatedAt = createdAtUpdated
            };

            // Act
            await _handler.Handle(updateCommand, CancellationToken.None);
            var result = await DbContext.VisitDocuments.FindAsync(visitDocument.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.AppointmentId);
            Assert.Equal("XLSX", result.FileType);
            Assert.Equal(3, result.UploadedBy);
            Assert.Equal(createdAtUpdated, result.CreatedAt);
        }

        [Fact]
        public async Task SaveVisitDocumentCommandHandler_WithNewVisitDocument_ShouldSetCreatedAtToNow()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            var command = new SaveVisitDocumentCommand
            {
                Id = 0,
                AppointmentId = 1,
                FilePath = "/files/document3.pdf",
                FileType = "PDF",
                UploadedBy = 1,
                CreatedAt = DateTime.Now  // The handler will override the initial DateTime.Now with this value
            };

            // Act
            await _handler.Handle(command, CancellationToken.None);
            var documents = DbContext.VisitDocuments.ToList();

            var afterCreation = DateTime.Now;

            // Assert
            Assert.NotEmpty(documents);
            var document = documents[documents.Count - 1];
            // The handler sets CreatedAt initially but then overrides it with the request value
            // So we verify that it's set to the command's CreatedAt
            Assert.True(document.CreatedAt >= beforeCreation && document.CreatedAt <= afterCreation.AddSeconds(1));
        }

        [Fact]
        public async Task SaveVisitDocumentCommandHandler_MultipleVisitDocuments_ShouldHandleMultipleOperations()
        {
            // Arrange
            var command1 = new SaveVisitDocumentCommand
            {
                Id = 0,
                AppointmentId = 1,
                FilePath = "/files/doc1.pdf",
                FileType = "PDF",
                UploadedBy = 1,
                CreatedAt = DateTime.Now
            };

            var command2 = new SaveVisitDocumentCommand
            {
                Id = 0,
                AppointmentId = 2,
                FilePath = "/files/doc2.docx",
                FileType = "DOCX",
                UploadedBy = 2,
                CreatedAt = DateTime.Now
            };

            // Act
            var result1 = await _handler.Handle(command1, CancellationToken.None);
            var result2 = await _handler.Handle(command2, CancellationToken.None);
            var documentsCount = DbContext.VisitDocuments.Count();

            // Assert
            Assert.False(result1.HasErrors);
            Assert.False(result2.HasErrors);
            Assert.Equal(2, documentsCount);
        }
    }
}
