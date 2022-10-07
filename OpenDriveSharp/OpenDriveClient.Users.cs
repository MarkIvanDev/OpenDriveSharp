using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace OpenDriveSharp
{
    public partial class OpenDriveClient
    {
        public async Task<OpenDriveResult> GetUserInfo(int? apply_bw = null, int? branding = null)
        {
            var query = new NameValueCollection();
            query.AddOptionalParameter(nameof(apply_bw), apply_bw);
            query.AddOptionalParameter(nameof(branding), branding);
            return await client.Get<UserInfoResult>(
                ApiEndpoints.GetRequestUri(ApiEndpoints.Users.Info(await GetSessionId().ConfigureAwait(false)), query)).ConfigureAwait(false);
        }
    }

    public class UserInfoResult : SuccessfulResult
    {
        public int UserId { get; set; }
        public int AccessUserId { get; set; }
        public string Username { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string PrivateKey { get; set; }
        public string Trial { get; set; }
        public long UserSince { get; set; }
        public long BwResetLast { get; set; }
        public string AccType { get; set; }
        public long MaxStorage { get; set; }
        public long StorageUsed { get; set; }
        public long BwMax { get; set; }
        public long BwUsed { get; set; }
        public string FVersioning { get; set; }
        public string FVersions { get; set; }
        public int DailyStat { get; set; }
        public string UserLang { get; set; }
        public long MaxFileSize { get; set; }
        public string Level { get; set; }
        public int Enable2FA { get; set; }
        public string UserPlan { get; set; }
        public string TimeZone { get; set; }
        public int MaxAccountUsers { get; set; }
        public int IsAccountUser { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public string AvatarColor { get; set; }
        public int AdminMode { get; set; }
        public string DueDate { get; set; }
        public string WebLink { get; set; }
        public int PublicProfiles { get; set; }
        public int RootFolderPermission { get; set; }
        public int CanChangePwd { get; set; }
        public int IsPartner { get; set; }
        public string Partner { get; set; }
        public string SupportUrl { get; set; }
        public string PartnerUsersDomain { get; set; }
        public bool Suspended { get; set; }
        public string Affiliation { get; set; }
        public string UserUID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PopUp { get; set; }
        public string InvoiceAddress { get; set; }
        public string UserAddress { get; set; }
        public string CardAddress { get; set; }
        public string Verified { get; set; }
        public string SignupVerified { get; set; }
        public string BrandingDriveName { get; set; }
        public string BrandingMenuBgColor { get; set; }
        public string BrandingMenuFontColor { get; set; }
        public string BrandingSubdomain { get; set; }
    }
}
