namespace Dicer.Constants
{
    public sealed class Constants
    {
        public const string roleIdClient = "2329f4a9-beb0-4c61-90f0-e98f2941dc38";
        public const string roleIdCreator = "f306c174-b808-4256-a23a-99a932193477";

        #region SP
        public const string getProvinsi = "exec GET_PROVINSI";
        public const string getKota = "exec GET_KOTA @ProvinsiId";
        #endregion SP
    }
}
