using System.ComponentModel;

namespace ClinicaWebApp.Models.Usuario.Medico
{
    public class CitaMedico
    {
        [DisplayName("CODIGO CITA")]
        public long ide_cit { get; set; }
        [DisplayName("FECHA")]
        public DateTime cal_cit { get; set; }
        [DisplayName("CONSULTORIO")]
        public int con_cit { get; set; }
        [DisplayName("PACIENTE")]
        public string paciente { get; set; }
        [DisplayName("MONTO PAGAR")]
        public decimal mon_pag { get; set; }
    }
}
