namespace ClinicaAPI.Models.Usuario.Paciente
{
    public class CitaPaciente
    {
        public long ide_cit { get; set; }
        public DateTime cal_cit { get; set; }
        public int con_cit { get; set; }
        public string medico { get; set; }
        public string nom_esp { get; set; }
        public decimal mon_pag { get; set; }
    }
}
