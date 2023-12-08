namespace BeautySales.AplicacionWeb.Utilidades.Response
{
    //Esta clase es donde se manejaran todas las respuestas al sistema
    public class GenericResponse<TObject>
    {
        public bool Estado { get; set; }
        public string? Mensaje { get; set; }
        public TObject? Objeto { get; set; }
        public List<TObject>? ListaObjeto { get; set; }
    }
}
