namespace BlazorDemo_WebAPI.Helpers
{
    public class Misc
    {
        public T CastToType<T>(object objType, T type) where T : class
        {
            var cast = objType as T;

            if (cast == null)
                throw new InvalidCastException();

            return cast;
        }
    }
}
