namespace CustomerAPI.Seedwork
{
    public static class Preconditions
    {
        public static void Requires(bool value, string message = "BUGGG!!!")
        {
            if (!value) throw new ContractException(message);
        }
    }
}
