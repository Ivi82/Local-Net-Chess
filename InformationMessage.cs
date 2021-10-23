namespace Chess
{

    public static class InformationMessage
    {
        internal delegate void Delegate();
        internal static event Delegate NewInformationMessage;
        internal static string message;

        internal static void Set(string newMessage)
        {
            message = newMessage;
            NewInformationMessage();

        }

    }
}
