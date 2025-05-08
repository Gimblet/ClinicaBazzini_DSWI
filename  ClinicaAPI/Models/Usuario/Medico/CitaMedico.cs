namespace ClinicaAPI.Models.Usuario.Medico
{
    public class CitaMedico
    {
        public long ide_cit { get; set; }
        public DateTime cal_cit { get; set; }
        public int con_cit { get; set; }
        public string paciente { get; set; }
        public decimal mon_pag { get; set; }
    }
}
