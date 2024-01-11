using Newtonsoft.Json;

namespace Models
{
    public enum TipoMensagem
    {
            Informação, Erro
    }
    public class MensagemModel
    {
        public TipoMensagem Tipo { get; set; }
        public string Texto { get; set; }
        public MensagemModel(string mensangem, TipoMensagem tipo = TipoMensagem.Informação)
        {
            this.Tipo = tipo;
            this.Texto = mensangem;
        }

        public static string Serializar(string mensangem, TipoMensagem tipo = TipoMensagem.Informação)
        {
            var mensangemModel = new MensagemModel(mensangem, tipo);
            return JsonConvert.SerializeObject(mensangemModel);
        }
        public static MensagemModel Desserializar(string mensangemString)
        {
            return JsonConvert.DeserializeObject<MensagemModel>(mensangemString);
        }

    }
}
