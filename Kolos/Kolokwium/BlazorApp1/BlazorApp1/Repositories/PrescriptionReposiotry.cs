using System.Data;
using System.Data.SqlClient;

namespace WebApplication2.Controllers;

public interface IPrescriptionReposiotry
{
    Task<Medication> GetInformationAboutPrescriibtion(int idMedication);
    Task DeletePatient(int idPatient);
}

public class PrescriptionReposiotry
{
    private readonly IConfiguration _configuration;
    public PrescriptionReposiotry(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Medication> GetInformationAboutPrescriibtion(int idMedication)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();
        
        await using var transaction = await connection.BeginTransactionAsync();
        
        try
        {
            var query = "SELECT Prescription_Medication.idMedication,Prescription.Date FROM Prescription_Medication " +
                        "INNER JOIN Prescription on Prescription.IdPrescription = Prescription_Medication.IdPrescription" +
                        "WHERE IdMedication = @IDMedication" +
                        "ORDER BY Prescription.Date DESC";
            await using var command = new SqlCommand(query, connection);
            command.Transaction = (SqlTransaction)transaction;
            command.Parameters.AddWithValue("@IDMedication", idMedication);
            await command.ExecuteNonQueryAsync();
            Medication idMedicationInfro = (int)await command.ExecuteScalarAsync();
            await transaction.CommitAsync();
            return idMedicationInfro;
        }
        catch
        {
            await transaction.RollbackAsync();
            return null;
        }
    }

    public async Task DeletePatient(int idPatient)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();
        
        await using var transaction = await connection.BeginTransactionAsync();
        
        try
        {
            var query = "DELTE FROM Patient WHERE IdPatient = @IdPacjenta";
            await using var command = new SqlCommand(query, connection);
            command.Transaction = (SqlTransaction)transaction;
            command.Parameters.AddWithValue("@IdPacjenta", idPatient);
            await command.ExecuteNonQueryAsync();
            
        }
        catch
        {
            await transaction.RollbackAsync();
        }
    }
}