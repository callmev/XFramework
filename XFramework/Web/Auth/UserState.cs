namespace XFramework.Web.Auth
{
    public enum UserState
    {
        /// <summary>
        /// ����
        /// </summary>
        Normal = 0,

        /// <summary>
        /// δ��֤
        /// </summary>
        NotAuthenticated = -1,

        /// <summary>
        /// ��Ȩ��
        /// </summary>
        NotAuthority = -2
    }
}