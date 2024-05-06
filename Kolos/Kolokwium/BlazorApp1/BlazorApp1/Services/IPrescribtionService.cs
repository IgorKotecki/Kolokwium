namespace WebApplication2.Controllers;

public interface IPrescribtionService
{
    public Task<Medication> GetInformationAboutPrescriibtion(PrescriptionDto dto );
    public Task DeletePatient(int idPatient);
}

public class PrescribtionService : IPrescribtionService
{
    private readonly IPrescriptionReposiotry _prescriptionReposiotry;

    public PrescribtionService(IPrescriptionReposiotry prescribtionService)
    {
        _prescriptionReposiotry = prescribtionService;
    }

    public async Task<Medication> GetInformationAboutPrescriibtion(PrescriptionDto dto)
    {
        const int idMedication = 1;

        var idMedicationInfo = await _prescriptionReposiotry.GetInformationAboutPrescriibtion(
            idMedication: dto.IdMedication!.Value);

        if (!idMedicationInfo.HasValue)
            throw new Exception("Faild to get the mediacrion");
        return idMedicationInfo.Value;
    }
}