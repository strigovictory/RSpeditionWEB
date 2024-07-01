namespace RSpeditionWEB.Proto
{
    public static class CustomTypesExtension
    {
        private static decimal NanoFactor = 1_000_000_000;

        public static decimal GetDecimalValue(this customTypesGRPC.DecimalValue grpcDecimal)
            => grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
    }
}
