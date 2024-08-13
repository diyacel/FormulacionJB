namespace RecepciónPesosJamesBrown.Helpers
{
    public class UnitConverter
    {
        public static double ConvertKgToG(double cantidad)
        {
            return cantidad * 1000;
        }
        public static double ConvertGToKg(double cantidad)
        {
            return cantidad / 1000;
        }
        public static double ConvertMgToKg(double cantidad)
        {
            return cantidad / 1000000;
        }
    }
}
