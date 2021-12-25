namespace HepsiBurada.Core.Extensions
{
    public static class AutoMapperExtensions
    {
        public static TDest MapTo<TDest>(this object src)
        {
            return (TDest)AutoMapper.Mapper.Map(src, src.GetType(), typeof(TDest));
        }
    }
}
