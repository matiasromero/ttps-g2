using VacunassistBackend.Entities.Vaccines.Calendar;
using VacunassistBackend.Entities.Vaccines.Seasonal;
using VacunassistBackend.Entities.Vaccines.Pandemic;

namespace VacunassistBackend.Entities.Vaccines;

public static class Vaccines
{
    // CALENDARdo
    public static Vaccine A_Tuberculosis = new A_Tuberculosis();
    public static Vaccine B_HepatitisB = new B_HepatitisB();
    public static Vaccine C_Neumococo = new C_Neumococo();
    public static Vaccine D_Quintuple = new D_Quintuple();
    public static Vaccine E_Polio = new E_Polio();
    public static Vaccine F_Rotavirus = new F_Rotavirus();
    public static Vaccine G_Meningococo = new G_Meningococo();
    public static Vaccine H_TripleViral = new H_TripleViral();
    public static Vaccine I_HepatitisA = new I_HepatitisA();
    public static Vaccine J_Varicela = new J_Varicela();
    public static Vaccine K_TripleBacteriana = new K_TripleBacteriana();
    public static Vaccine L_VPH = new L_VPH();
    public static Vaccine M_FiebreAmarilla = new M_FiebreAmarilla();
    public static Vaccine N_FiebreHemorragica = new N_FiebreHemorragica();

    // PANDEMIC

    public static Vaccine O_COVID = new O_COVID();

    // SEASONAL
    public static Vaccine P_Antigripal = new P_Antigripal();

    private static readonly Vaccine[] all = new[]
           {
                A_Tuberculosis, B_HepatitisB, C_Neumococo, D_Quintuple, E_Polio, F_Rotavirus, G_Meningococo, H_TripleViral, I_HepatitisA,
                J_Varicela, K_TripleBacteriana, L_VPH, M_FiebreAmarilla, N_FiebreHemorragica
            };

    public static Vaccine[] All
    {
        get { return all; }
    }

    public static Vaccine[] Calendar = all.Where(x => x.Type == VaccineType.Calendar).ToArray();
    public static Vaccine[] Pandemic = all.Where(x => x.Type == VaccineType.Pandemic).ToArray();
    public static Vaccine[] Seasonal = all.Where(x => x.Type == VaccineType.Seasonal).ToArray();

    public static Vaccine Get(int id)
    {
        return all.FirstOrDefault(x => x.Id == id);
    }
}
