namespace BTL_QuanLiBanSach.Helpers
{
    public static class CodeGenerator
    {
        public static string GenerateOrderCode()
        {
            var datePart = DateTime.Now.ToString("yyyyMMdd");
            var randomPart = new Random().Next(100, 999);
            return $"ORD-{datePart}-{randomPart}";
        }
    }
}
