# Migration Instructions for DataAnnotations

## Summary of Changes Made

The following updates have been applied to the KooliProjekt codebase:

### 1. **Data Models Enhanced with DataAnnotations**
   All data models in `KooliProjekt.Application/Data/` have been updated with proper validation attributes:

   - **Doctor.cs** - Added Required, MaxLength, MinLength with error messages
   - **User.cs** - Added Required, EmailAddress, Phone, MaxLength validations
   - **Appointment.cs** - Added Required, Range validations for foreign keys
   - **Availability.cs** - Added Required, Range, MaxLength validations
   - **Service.cs** - Added Required, Range, MaxLength, MinLength validations
   - **VisiteDocument.cs** - Added Required, Range, MaxLength, MinLength validations
   - **InvoiceLine.cs** - Removed unused `Lines` property, added proper validations
   - **AdminOverride.cs** - Removed unused properties, added proper validations
   - **Invoice.cs** - Already has proper validations

### 2. **Query Handlers Fixed**
   - **DoctorsQueryHandler** - Changed ordering from `Time` to `Name`
   - **AppointmentsQueryHandler** - Uses proper Time ordering
   - **AvailabilityQueryHandler** - Changed to order by `Date` and uses `global::Availability` to avoid namespace conflicts
   - **AdminOverrideQueryHandler** - Fixed to return `PagedResult<AdminOverride>` instead of `AdminOverrideQuery`
   - **InvoiceQueryHandler** - Confirmed it uses proper `InvoiceDate` ordering

### 3. **API Controllers Updated**
   All controllers now accept pagination parameters:

   - **DoctorsController** - Updated to accept `page` and `pageSize` query parameters
   - **InvoiceController** - Updated to accept `page` and `pageSize` query parameters
   - **AppointmentsController** - Updated to accept `page` and `pageSize` query parameters
   - **AvailabilityController** - Updated to accept `page` and `pageSize` query parameters
   - **AdminOverrideController** - Updated to accept `page` and `pageSize` query parameters
   - **InvoiceLinesController** - Updated to accept `page` and `pageSize` query parameters
   - **ServicesController** - Updated to accept `page` and `pageSize` query parameters
   - **UsersController** - Updated to accept `page` and `pageSize` query parameters

### 4. **Command and Handler Fixes**
   - **SaveDoctorsCommandHandler** - Fixed to properly save Doctor properties
   - **SaveAppointmentsCommand** - Updated with proper properties (AppointmentTime, UserId, DoctorId)
   - **SaveAppointmentsCommandHandler** - Fixed to handle proper Appointment properties
   - **SaveAdminOverrideCommand** - Updated with proper properties (Start, End, DoctorId, CreatedBy)
   - **SaveAdminOverrideCommandHandler** - Fixed to properly map command properties to entity

## Next Steps: Create and Apply Migration

### Option 1: Using Package Manager Console (Recommended)
1. Open Visual Studio
2. Go to **Tools ? NuGet Package Manager ? Package Manager Console**
3. Run the following command:
   ```
   Add-Migration DataAnnotations -Project KooliProjekt.Application
   ```
4. This will create a new migration file in `KooliProjekt.Application/Migrations/`
5. Then run:
   ```
   Update-Database
   ```

### Option 2: Using .NET CLI (PowerShell)
1. Open **new** PowerShell window (important - the tool was just installed)
2. Navigate to the project directory:
   ```powershell
   cd "C:\Users\opilane\Documents\GitHub\Programmeerimine2"
   ```
3. Run the migration command:
   ```powershell
   dotnet ef migrations add DataAnnotations -p KooliProjekt.Application -s KooliProjekt.WebAPI
   ```
4. Apply the migration:
   ```powershell
   dotnet ef database update -p KooliProjekt.Application -s KooliProjekt.WebAPI
   ```

## Database Changes Expected

After applying the migration, the SQL Server database tables will have:

- **MaxLength constraints** on string columns
- **NOT NULL constraints** on required fields
- **Check constraints** for Range validations
- **Email validation** constraints where applicable
- **Default values** for boolean fields
- **Proper data types** for all columns

## Testing the Changes

### 1. Test Pagination
   ```
   GET http://localhost:5000/api/doctors?page=1&pageSize=10
   GET http://localhost:5000/api/invoices?page=1&pageSize=5
   GET http://localhost:5000/api/appointments?page=2&pageSize=10
   ```

### 2. Add Test Data
   You should insert test data into the database to verify pagination works:
   - At least 15+ records in each table
   - Then test different page values

### 3. Insert Sample Data (SQL)
   ```sql
   -- Add doctors
   INSERT INTO doctors (Name, Specialty) VALUES 
   ('Dr. John Smith', 'Cardiology'),
   ('Dr. Sarah Johnson', 'Dermatology'),
   ('Dr. Michael Brown', 'Orthopedics'),
   ...
   ```

## File Changes Summary

| File | Change Type | Description |
|------|------------|-------------|
| Doctor.cs | Updated | Added DataAnnotations |
| User.cs | Updated | Added DataAnnotations and length constraints |
| Appointment.cs | Updated | Added Required and Range validations |
| Availability.cs | Updated | Added DataAnnotations |
| Service.cs | Updated | Added Code field requirement and constraints |
| VisiteDocument.cs | Updated | Added comprehensive validations |
| InvoiceLine.cs | Updated | Removed unused Lines property, added validations |
| AdminOverride.cs | Updated | Removed unused properties, added validations |
| DoctorsQueryHandler.cs | Fixed | OrderBy Name instead of Time |
| AvailabilityQueryHandler.cs | Fixed | Uses global::Availability and orders by Date |
| AdminOverrideQueryHandler.cs | Fixed | Returns correct PagedResult<AdminOverride> type |
| InvoiceQueryHandler.cs | Verified | Confirmed using proper InvoiceDate ordering |
| SaveDoctorsCommandHandler.cs | Fixed | Proper property mapping |
| SaveAppointmentsCommand.cs | Updated | Added proper properties |
| SaveAppointmentsCommandHandler.cs | Fixed | Proper handler implementation |
| SaveAdminOverrideCommand.cs | Updated | Added proper properties |
| SaveAdminOverrideCommandHandler.cs | Fixed | Proper handler implementation |
| DoctorsController.cs | Updated | Accepts page and pageSize parameters |
| InvoiceController.cs | Updated | Accepts page and pageSize parameters |
| AppointmentsController.cs | Updated | Accepts page and pageSize parameters |
| AvailabilityController.cs | Updated | Accepts page and pageSize parameters |
| AdminOverrideController.cs | Updated | Accepts page and pageSize parameters |
| InvoiceLinesController.cs | Updated | Accepts page and pageSize parameters |
| ServicesController.cs | Updated | Accepts page and pageSize parameters |
| UsersController.cs | Updated | Accepts page and pageSize parameters |

## Verification Checklist

After applying the migration:

- [ ] Build solution successfully compiles
- [ ] Database migration applied without errors
- [ ] Can query all entities with pagination
- [ ] Test GET requests with different page/pageSize values
- [ ] All constraint validations are applied in database
- [ ] Column types match the C# properties
- [ ] Relationships and foreign keys are intact

## Troubleshooting

**Issue: "dotnet ef" command not found**
- Solution: Open a NEW PowerShell window after installation

**Issue: Migration conflicts**
- Solution: Check if there are pending migrations in Migrations folder
- Run: `dotnet ef migrations list`

**Issue: Database update fails**
- Solution: Ensure connection string is correct in appsettings.json
- Check SQL Server is running and accessible

**Issue: Old "Time" property errors**
- All references to invalid `Time` property on Doctor have been fixed
- Cleaned up unused `Title` and `Lines` properties

