namespace Dicer.Constants
{
    public sealed class Constants
    {
        public const string roleIdClient = "2329f4a9-beb0-4c61-90f0-e98f2941dc38";
        public const string roleIdCreator = "f306c174-b808-4256-a23a-99a932193477";
        public const string roleNameClient = "Client";
        public const string roleNameCreator = "Creator";

        public const string DefaultProfileImg = "default_profile.jpg";
        public const string DefaultCampaignImg = "Default.jpg";

        #region SMTP Email
        public const string fromMail = "dicerhostmail@gmail.com";
        public const string subjectConfirmation = "[Dicer] Selesaikan Pendaftaran";
        public const string smtpServer = "smtp-relay.sendinblue.com";
        public const int smtpPort = 587;
        public const string smtpLogin = "dicerhostmail@gmail.com";
        public const string smtpPassword = "s3pvwEUD7914ITGR";
        #endregion SMTP Email


        #region SP
        public const string getProvinsi = "exec GET_PROVINSI";
        public const string getKota = "exec GET_KOTA @ProvinsiId";
        public const string getDataIg = "exec GET_DataIg";
        public const string upsertCampaign = "exec UPSERT_CAMPAIGN";
        public const string getAcceptance = "exec GET_ACCEPTANCE";
        public const string acceptParticipant = "exec ACCEPT_PARTICIPANT";
        public const string declineParticipant = "exec DECLINE_PARTICIPANT";
        public const string paidCampaign = "exec PAID_CAMPAIGN";
        public const string pay = "exec PAY";
        public const string getSaldo = "exec GET_SALDO";
        public const string withdrawlSaldo = "exec WITHDRAWL_SALDO";
        public const string saveChat = "exec SAVE_CHAT";
        public const string uploadDraft = "exec UPLOAD_DRAFT";
        public const string postLink = "exec POST_LINK";
        #endregion SP
    }
}