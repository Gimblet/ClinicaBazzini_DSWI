namespace ClinicaAPI.Models.Usuario.Medico
{
    public class PacientePorMedico
    {
        public long ide_pac { get; set; }
        public string Paciente { get; set; }
        public string num_doc { get; set; }
        public DateTime fna_usr { get; set; }
        public string nom_doc { get; set; }
        public int Total_Citas { get; set; }
    }
}
