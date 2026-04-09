# API Testing Guide

## How to Test the Delete Operations

### Using Swagger UI (Recommended for Development)
1. Run the application
2. Navigate to `https://localhost:PORT/swagger/index.html`
3. Find the controller for the entity you want to delete
4. Click on the DELETE endpoint
5. Enter the ID of the entity you want to delete
6. Click "Try it out"
7. Verify the response shows success

### Using cURL

**Delete a Doctor:**
```bash
curl -X DELETE "https://localhost:7000/api/doctors/1" -H "Content-Type: application/json"
```

**Delete a User:**
```bash
curl -X DELETE "https://localhost:7000/api/users/1" -H "Content-Type: application/json"
```

**Delete a Service:**
```bash
curl -X DELETE "https://localhost:7000/api/services/1" -H "Content-Type: application/json"
```

**Delete an Appointment:**
```bash
curl -X DELETE "https://localhost:7000/api/appointments/1" -H "Content-Type: application/json"
```

**Delete Availability:**
```bash
curl -X DELETE "https://localhost:7000/api/availability/1" -H "Content-Type: application/json"
```

**Delete an Invoice:**
```bash
curl -X DELETE "https://localhost:7000/api/invoice/1" -H "Content-Type: application/json"
```

**Delete an Invoice Line:**
```bash
curl -X DELETE "https://localhost:7000/api/invoicelines/1" -H "Content-Type: application/json"
```

**Delete an Admin Override:**
```bash
curl -X DELETE "https://localhost:7000/api/adminoverride/1" -H "Content-Type: application/json"
```

**Delete a Visit Document:**
```bash
curl -X DELETE "https://localhost:7000/api/visitdocuments/1" -H "Content-Type: application/json"
```

### Using Postman

1. Create a new request
2. Set method to **DELETE**
3. Enter the URL: `http://localhost:PORT/api/[endpoint]/{id}`
4. Click **Send**
5. Review the response

### Expected Responses

**Successful Deletion (200 OK):**
```json
{
  "isSuccess": true,
  "errors": []
}
```

**Not Found (400 Bad Request):**
```json
{
  "isSuccess": false,
  "errors": ["Doctor not found"]
}
```

## Step-by-Step Testing Workflow

### 1. First Run (Database Initialization)
```
a. Delete the existing database from SQL Server
b. Start the application
c. The database is automatically created and populated with seed data
```

### 2. Verify Data
```
a. Open Swagger UI at /swagger
b. Use GET endpoints to list all entities
c. Verify that at least 10 records exist for each entity type
```

### 3. Test Delete Operations
```
a. Note the ID of an entity (e.g., Doctor with ID=1)
b. Call DELETE endpoint with that ID
c. Verify response is successful
d. Call GET endpoint again to confirm the entity is deleted
e. Repeat for all entity types
```

### 4. Test Error Handling
```
a. Try to delete a non-existent entity (e.g., DELETE /api/doctors/999)
b. Verify appropriate error message is returned
c. Verify database is not affected
```

## Key Verification Points

? **Database Creation**: Database is created automatically on first run
? **Seed Data**: All 10+ records per entity type are present
? **Delete Success**: Entities are successfully removed
? **Delete Verification**: Deleted entities no longer appear in GET requests
? **Error Handling**: Non-existent entities return proper error messages
? **Transaction Safety**: Database is not corrupted after deletions
? **API Responses**: All endpoints return proper HTTP status codes and JSON responses

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Database not created | Ensure SQL Server is running, check connection string |
| No seed data | Verify SeedData.Generate() is called in Program.cs |
| DELETE returns 404 | Check if entity ID exists in database |
| DELETE returns 500 | Check application logs for database errors |
| Swagger UI not available | Ensure app is running in Development environment |

## Database Cleanup

To start fresh with seed data:
```sql
-- In SQL Server Management Studio
USE [YourDatabaseName]
GO

-- Drop and recreate all tables
DROP TABLE IF EXISTS [VisitDocuments]
DROP TABLE IF EXISTS [AdminOverride]
DROP TABLE IF EXISTS [InvoiceLines]
DROP TABLE IF EXISTS [Invoice]
DROP TABLE IF EXISTS [Appointments]
DROP TABLE IF EXISTS [Availability]
DROP TABLE IF EXISTS [Doctors]
DROP TABLE IF EXISTS [Users]
DROP TABLE IF EXISTS [Services]
GO
```

Then restart the application to recreate the database and seed data.
