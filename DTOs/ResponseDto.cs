namespace SafyroPresupuestos.DTOs
{
    public class ResponseDto<T>
    {
        public bool Ok { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public T? Contenido { get; set; }
        public int? Total { get; set; }
        public object? Paginacion { get; set; }
    }
}
