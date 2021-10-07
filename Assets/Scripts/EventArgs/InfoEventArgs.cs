public class InfoEventArgs<T> : System.EventArgs
{
    public T Info;

    public InfoEventArgs()
    {
        Info = default(T);
    }

    public InfoEventArgs(T info)
    {
        Info = info;
    }
}