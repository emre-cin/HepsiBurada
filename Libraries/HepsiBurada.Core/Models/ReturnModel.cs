namespace HepsiBurada.Core.Models
{
    public class ReturnModel<TEntity> : IReturn
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TEntity Data { get; set; }
    }

    public class ReturnModel: IReturn
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public interface IReturn
    {

    }
}

