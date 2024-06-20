
namespace Marvel.Api.Helpers
{
    /// <summary>
    /// manejador de respuestas de API
    /// </summary>
    public class Response
    {
        /// <summary>
        /// estado
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Mensaje de respuesta
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Errores
        /// </summary>
        public dynamic? Errors { get; set; } = null;
    }
}
