namespace Supplier_SRM_MVC.Exceptions
{
    public class NotFoundDataException:Exception
    {
        public NotFoundDataException() { }
        public NotFoundDataException(string message): base(message) { }
        public NotFoundDataException(string message, Exception innerException): base(message, innerException) { }
    }
}
