using System.ComponentModel;

namespace ClinicaWebApp.Models.Usuario.Paciente
{
    public class CitaPaciente
    {
        [DisplayName("ID CITA")]
        public long ide_cit { get; set; }
        [DisplayName("FECHA")]
        public DateTime cal_cit { get; set; }
        [DisplayName("CONSULTORIO")]
        public int con_cit { get; set; }
        [DisplayName("MEDICO")]
        public string medico { get; set; }
        [DisplayName("ESPECIALIDAD")]
        public string nom_esp { get; set; }
        [DisplayName("MONTO A PAGAR")]
        public decimal mon_pag { get; set; }
    }
}
