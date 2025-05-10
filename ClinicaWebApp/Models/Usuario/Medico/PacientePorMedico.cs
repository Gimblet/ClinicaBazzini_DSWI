using System.ComponentModel;

namespace ClinicaWebApp.Models.Usuario.Medico
{
    public class PacientePorMedico
    {
        [DisplayName("ID PACIENTE")]
        public long ide_pac { get; set; }
        [DisplayName("PACIENTE")]
        public string Paciente { get; set; }
        [DisplayName("N° DOCUMENTO")]
        public string num_doc { get; set; }
        [DisplayName("FEC. NACIMIENTO")]
        public DateTime fna_usr { get; set; }
        [DisplayName("DOCUMENTO")]
        public string nom_doc { get; set; }
        [DisplayName("TOTAL CITA")]
        public int Total_Citas { get; set; }
    }
}
