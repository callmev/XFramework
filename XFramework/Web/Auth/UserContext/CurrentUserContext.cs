namespace XFramework.Web.Auth.UserContext
{
    public class CurrentUserContext : IUserContext
    {
        //public string GmpLeaRunWebapi
        //{
        //    get
        //    {
        //        if (string.IsNullOrWhiteSpace(EnvironmentConfig.Instance.GmpLeaRunWebapi))
        //            throw new Exception("GmpLeaRunWebapi 地址节点配置不正确");
        //        return EnvironmentConfig.Instance.GmpLeaRunWebapi.Trim('/');
        //    }
        //}

        //public string Bdm
        //{
        //    get
        //    {
        //        if (string.IsNullOrWhiteSpace(EnvironmentConfig.Instance.Bdm))
        //            throw new Exception("Bdm 地址节点配置不正确");
        //        return EnvironmentConfig.Instance.Bdm.Trim('/');
        //    }
        //}

        ///// <summary>
        /////     获取当前用户的认证信息
        ///// </summary>
        ///// <param name="userId">用户 Id</param>
        ///// <param name="token"></param>
        ///// <param name="publicKey"></param>
        ///// <returns></returns>
        //public CustomAuth.CustomAuth GetCustomAuth(string userId, string token, string publicKey)
        //{
        //    var customAuthStoreId = KeyGeneration.GenerateAuthenticationStoreKey(userId, token, publicKey);

        //    return RedisClientWrapper.Instance.CacheClient.Get<CustomAuth.CustomAuth>(customAuthStoreId);
        //}

        ///// <summary>
        /////     获取当前用户的基本信息
        ///// </summary>
        ///// <param name="userId">用户 Id</param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //public UserInfo GetUserInfo(string userId, string pubKey, string tokenId)
        //{
        //    var userInfoStoreId = KeyGeneration.GenerateUserInfoStoreKey(userId);
        //    var userInfo = RedisClientWrapper.Instance.CacheClient.Get<UserInfo>(userInfoStoreId);

        //    if (userInfo != null)
        //    {
        //        return userInfo;
        //    }

        //    return GetCurrentUserCache(userId, pubKey, tokenId);
        //}

        //public UserInfo GetUserInfo(string queryUserId, string userId, string pubKey, string tokenId)
        //{
        //    var userInfoStoreId = KeyGeneration.GenerateUserInfoStoreKey(queryUserId);
        //    var userInfo = RedisClientWrapper.Instance.CacheClient.Get<UserInfo>(userInfoStoreId);

        //    if (userInfo != null)
        //    {
        //        return userInfo;
        //    }

        //    return GetCurrentUserCache(queryUserId, userId, pubKey, tokenId);
        //}

        ///// <summary>
        /////     获取用户的部门信息
        ///// </summary>
        ///// <param name="departmentId">部门 Id</param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //public DepartmentInfoCollection GetDepartmentInfoCollection(string departmentId, string userId, string pubKey,
        //    string tokenId)
        //{
        //    var departmentInfoCollectionStoreId =
        //        KeyGeneration.GenerateUserDepartmentInfoCollectionStoreKey(userId);

        //    var departmentInfoCollection =
        //        RedisClientWrapper.Instance.CacheClient.Get<DepartmentInfoCollection>(
        //            departmentInfoCollectionStoreId);
        //    return departmentInfoCollection ??
        //           GetCurrentUserDepartmentInfoCollectionCache(departmentId, userId, pubKey, tokenId);
        //}

        ///// <summary>
        /////     根据角色 id 获取用户的角色信息
        ///// </summary>
        ///// <param name="roleId">角色 Id</param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //public RoleInfo GetRoleInfo(string roleId, string userId, string pubKey, string tokenId)
        //{
        //    var userRoleInfoStoreId = KeyGeneration.GenerateRoleInfoStoreKey(userId, roleId);

        //    var roleInfo = RedisClientWrapper.Instance.CacheClient.Get<RoleInfo>(userRoleInfoStoreId);

        //    return roleInfo ?? GetCurrentUserRoleInfo(roleId, userId, pubKey, tokenId);
        //}

        ///// <summary>
        /////     获取用户的角色信息
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //public RoleInfoCollection GetRoleInfoCollection(string userId, string pubKey, string tokenId)
        //{
        //    var userRoleInfoStoreId = KeyGeneration.GenerateUserRoleInfoStoreKey(userId);

        //    var roleInfos = RedisClientWrapper.Instance.CacheClient.Get<RoleInfoCollection>(userRoleInfoStoreId);
        //    return roleInfos ?? GetCurrentUserRoleInfoCollection(userId, pubKey, tokenId);
        //}

        ///// <summary>
        /////     根据码值获取码表信息
        ///// </summary>
        ///// <param name="code">码值</param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //public DataDictionaryDetailInfo GetDataDictionaryDetailInfo(string code, string userId, string pubKey,
        //    string tokenId)
        //{
        //    var dataDictionaryDetailInfoStoreId = KeyGeneration.GenerateGetDataDictionaryDetailInfoStoreKey(code);

        //    var dataDictionaryDetailInfo =
        //        RedisClientWrapper.Instance.CacheClient.Get<DataDictionaryDetailInfo>(
        //            dataDictionaryDetailInfoStoreId);
        //    return dataDictionaryDetailInfo ?? GetDataDictionaryInfo(code, userId, pubKey, tokenId);
        //}

        ///// <summary>
        /////     根据数据权限 Key 获取数据权限集合
        ///// </summary>
        ///// <param name="moduleId"></param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //public DataPermissionInfoCollection GetDataPermissionInfoCollection(string moduleId, string userId,
        //    string pubKey, string tokenId)
        //{
        //    var dataStoreId =
        //        KeyGeneration.GenerateLeaRunDataAuthorizationStoreKey(userId, moduleId);

        //    var dataPermissionResponse =
        //        RedisClientWrapper.Instance.CacheClient.Get<DataPermissionInfoCollection>(dataStoreId);
        //    return dataPermissionResponse ?? GetDataPermissionCollection(moduleId, userId, pubKey, tokenId);
        //}

        ///// <summary>
        /////     根据模块权限 Key 获取模块权限数据
        ///// </summary>
        ///// <param name="moduleId"></param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //public FieldPermissionInfoCollection GetFieldPermissionInfoCollection(
        //    string moduleId, string userId, string pubKey, string tokenId)
        //{
        //    var dataStoreId =
        //        KeyGeneration.GenerateLeaRunFieldAuthorizationStoreKey(userId, moduleId);

        //    var fieldPermissionInfoCollection =
        //        RedisClientWrapper.Instance.CacheClient.Get<FieldPermissionInfoCollection>(dataStoreId);
        //    return fieldPermissionInfoCollection ?? GetFieldPermissionCollection(moduleId, userId, pubKey, tokenId);
        //}

        //public FieldPermissionInfoCollection GetFieldPermissionInfoCollection(ApiRequest apiRequest)
        //{
        //    var dataStoreId = KeyGeneration.GenerateLeaRunFieldAuthorizationStoreKey(apiRequest.UserId,
        //        apiRequest.ModuleId);

        //    var fieldPermissionInfoCollection =
        //        RedisClientWrapper.Instance.CacheClient.Get<FieldPermissionInfoCollection>(dataStoreId);
        //    return fieldPermissionInfoCollection ?? GetFieldPermissionCollection(
        //        apiRequest.ModuleId, apiRequest.UserId, apiRequest.PubKey, apiRequest.TokenId);
        //}

        ///// <summary>
        /////     根据功能权限 Key 获取功能权限数据
        ///// </summary>
        ///// <param name="moduletype"></param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //public FunctionPermissionInfoCollection GetFunctionPermissionInfoCollection(string moduletype, string userId,
        //    string pubKey, string tokenId)
        //{
        //    var functionStoreId =
        //        KeyGeneration.GenerateLeaRunFunctionAuthorizationStoreKey(userId, moduletype);

        //    var functionPermissionInfoCollection =
        //        RedisClientWrapper.Instance.CacheClient.Get<FunctionPermissionInfoCollection>(functionStoreId);
        //    return functionPermissionInfoCollection ?? GetFunctionCollection(moduletype, userId, pubKey, tokenId);
        //}

        ///// <summary>
        /////     根据操作权限 Key 获取操作权限数据
        ///// </summary>
        ///// <param name="moduletype"></param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //public OperationPermissionInfoCollection GetOperationPermissionInfoCollection(string moduletype, string userId,
        //    string pubKey, string tokenId)
        //{
        //    var functionStoreId =
        //        KeyGeneration.GenerateLeaRunFunctionAuthorizationStoreKey(userId, moduletype);

        //    var operationPermissionInfoCollection =
        //        RedisClientWrapper.Instance.CacheClient.Get<OperationPermissionInfoCollection>(functionStoreId);
        //    return operationPermissionInfoCollection ??
        //           GeOperationPermissionCollection(moduletype, userId, pubKey, tokenId);
        //}

        ///// <summary>
        /////     根据产品 Id 获取产品信息
        ///// </summary>
        ///// <param name="productId"></param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //public ProductInfo GetProductInfo(string productId, string userId, string pubKey, string tokenId)
        //{
        //    var generateProductInfoStoreKey = KeyGeneration.GenerateProductInfoStoreKey(productId);
        //    var productInfo = RedisClientWrapper.Instance.CacheClient.Get<ProductInfo>(generateProductInfoStoreKey);

        //    return productInfo ?? GetProduct(productId, userId, pubKey, tokenId);
        //}

        //#region Get data from url

        ///// <summary>
        /////     获取用户缓存
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //private UserInfo GetCurrentUserCache(string userId, string pubKey, string tokenId)
        //{
        //    var storeId = KeyGeneration.GenerateUserInfoStoreKey(userId);
        //    var userInfo = RedisClientWrapper.Instance.CacheClient.Get<UserInfo>(storeId);
        //    if (userInfo != null) return userInfo;

        //    var infoCollectionWebResponse = GetWithHttpClient<UserInfoWebResponse>(
        //        string.Format("{0}/api/users/{1}", GmpLeaRunWebapi, userId), userId,
        //        pubKey, tokenId);

        //    if (infoCollectionWebResponse == null) return null;

        //    RedisClientWrapper.Instance.CacheClient.Set(storeId, infoCollectionWebResponse.User);
        //    return infoCollectionWebResponse.User;
        //}

        ///// <summary>
        /////     缓存用户部门数据
        ///// </summary>
        ///// <param name="departmentId"></param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        //private DepartmentInfoCollection GetCurrentUserDepartmentInfoCollectionCache(string departmentId,
        //    string userId, string pubKey, string tokenId)
        //{
        //    var storeId = KeyGeneration.GenerateUserDepartmentInfoCollectionStoreKey(userId);
        //    var departmentInfoCollection =
        //        RedisClientWrapper.Instance.CacheClient.Get<DepartmentInfoCollection>(storeId);
        //    if (departmentInfoCollection != null) return departmentInfoCollection;

        //    var infoCollectionWebResponse = GetWithHttpClient<DepartmentResponse>(
        //        string.Format("{0}/api/departments/{1}", GmpLeaRunWebapi, departmentId), userId,
        //        pubKey, tokenId);

        //    if (infoCollectionWebResponse == null) return null;

        //    RedisClientWrapper.Instance.CacheClient.Set(storeId,
        //        new DepartmentInfoCollection {infoCollectionWebResponse.DepartmentInfo});
        //    return new DepartmentInfoCollection {infoCollectionWebResponse.DepartmentInfo};
        //}

        ///// <summary>
        /////     缓存用户角色数据
        ///// </summary>
        ///// <param name="roleId"></param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        //private RoleInfo GetCurrentUserRoleInfo(string roleId, string userId, string pubKey, string tokenId)
        //{
        //    var storeId = KeyGeneration.GenerateRoleInfoStoreKey(userId, roleId);
        //    var roleInfo = RedisClientWrapper.Instance.CacheClient.Get<RoleInfo>(storeId);
        //    if (roleInfo != null) return roleInfo;


        //    var detailInfoWebResponse = GetWithHttpClient<RoleResponse>(
        //        string.Format("{0}/api/roles/{1}", GmpLeaRunWebapi, roleId), userId, pubKey, tokenId);

        //    if (detailInfoWebResponse == null) return null;

        //    RedisClientWrapper.Instance.CacheClient.Set(storeId, detailInfoWebResponse.Role);
        //    return detailInfoWebResponse.Role;
        //}

        ///// <summary>
        /////     缓存用户角色数据
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        //private RoleInfoCollection GetCurrentUserRoleInfoCollection(string userId, string pubKey, string tokenId)
        //{
        //    var storeId = KeyGeneration.GenerateUserRoleInfoStoreKey(userId);
        //    var roleInfoCollection = RedisClientWrapper.Instance.CacheClient.Get<RoleInfoCollection>(storeId);
        //    if (roleInfoCollection != null) return roleInfoCollection;


        //    var response = GetWithHttpClient<RoleResponse>(
        //        string.Format("{0}/api/roles/user?UserSelectId={1}", GmpLeaRunWebapi, userId), userId, pubKey,
        //        tokenId);

        //    if (response == null || response.RoleCollection == null || !response.RoleCollection.Any()) return null;

        //    RedisClientWrapper.Instance.CacheClient.Set(storeId, response.RoleCollection);
        //    return response.RoleCollection;
        //}

        ///// <summary>
        /////     缓存字典信息
        ///// </summary>
        ///// <param name="code"></param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //private DataDictionaryDetailInfo GetDataDictionaryInfo(
        //    string code, string userId, string pubKey, string tokenId)
        //{
        //    var storeId = KeyGeneration.GenerateGetDataDictionaryDetailInfoStoreKey(code);
        //    var dataDictionaryDetailInfo = RedisClientWrapper.Instance.CacheClient.Get<DataDictionaryDetailInfo>(storeId);
        //    if (dataDictionaryDetailInfo != null) return dataDictionaryDetailInfo;

        //    var dataDictionaryDetailInfoWebResponse =
        //        GetWithHttpClient<DataDictionaryDetailInfoWebResponse>(
        //            string.Format("{0}/api/datadictionaries/{1}", GmpLeaRunWebapi, code), userId, pubKey, tokenId);

        //    if (dataDictionaryDetailInfoWebResponse == null) return null;

        //    RedisClientWrapper.Instance.CacheClient.Set(
        //        storeId, dataDictionaryDetailInfoWebResponse.DataDictionaryDetailInfo);
        //    return dataDictionaryDetailInfoWebResponse.DataDictionaryDetailInfo;
        //}

        ///// <summary>
        /////     Http 请求
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="url"></param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //private T GetWithHttpClient<T>(string url, string userId, string pubKey, string tokenId)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Add("Accept", "application/json");
        //        client.DefaultRequestHeaders.Add("UserId", userId);
        //        client.DefaultRequestHeaders.Add("PubKey", pubKey);
        //        client.DefaultRequestHeaders.Add("TokenId", tokenId);
        //        var readAsStringAsync = client.GetAsync(url).Result.Content.ReadAsStringAsync();

        //        Task.WaitAll(readAsStringAsync);

        //        return JsonConvert.DeserializeObject<T>(readAsStringAsync.Result);
        //    }
        //}

        ///// <summary>
        /////     通过 Http 获取功能权限数据并缓存
        ///// </summary>
        ///// <param name="moduletype"></param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //private FunctionPermissionInfoCollection GetFunctionCollection(string moduletype, string userId, string pubKey,
        //    string tokenId)
        //{
        //    var response = GetWithHttpClient<FunctionPermissionResponse>(
        //        string.Format("{0}/api/permissions/functions?userid={1}&ModuleType={2}", GmpLeaRunWebapi, userId,
        //            moduletype), userId, pubKey, tokenId);
        //    if (response == null || response.ResponseCollection == null || !response.ResponseCollection.Any())
        //        return null;
        //    var storeId = KeyGeneration.GenerateLeaRunFunctionAuthorizationStoreKey(userId, moduletype);
        //    RedisClientWrapper.Instance.CacheClient.Set(storeId, response.ResponseCollection);

        //    return response.ResponseCollection;
        //}

        ///// <summary>
        /////     通过 Http 获取操作权限数据并缓存
        ///// </summary>
        ///// <param name="moduleId"></param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //private OperationPermissionInfoCollection GeOperationPermissionCollection(string moduleId, string userId,
        //    string pubKey, string tokenId)
        //{
        //    var response = GetWithHttpClient<OperationPermissionResponse>(
        //        string.Format("{0}/api/permissions/operations?userid={1}&moduleId={2}", GmpLeaRunWebapi, userId,
        //            moduleId), userId, pubKey, tokenId);
        //    if (response == null || response.ResponseCollection == null || !response.ResponseCollection.Any())
        //        return null;
        //    var storeId = KeyGeneration.GenerateLeaRunOperationAuthorizationStoreKey(userId, moduleId);
        //    RedisClientWrapper.Instance.CacheClient.Set(storeId, response.ResponseCollection);

        //    return response.ResponseCollection;
        //}

        ///// <summary>
        /////     通过 Http 获取模块权限数据并缓存
        ///// </summary>
        ///// <param name="moduleId"></param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //private FieldPermissionInfoCollection GetFieldPermissionCollection(string moduleId, string userId, string pubKey,
        //    string tokenId)
        //{
        //    var response = GetWithHttpClient<FieldPermissionResponse>(
        //        string.Format("{0}/api/permissions/fields?userid={1}&moduleId={2}", GmpLeaRunWebapi, userId,
        //            moduleId), userId, pubKey, tokenId);
        //    if (response == null || response.ResponseCollection == null || !response.ResponseCollection.Any())
        //        return null;
        //    var storeId = KeyGeneration.GenerateLeaRunFieldAuthorizationStoreKey(userId, moduleId);
        //    RedisClientWrapper.Instance.CacheClient.Set(storeId, response.ResponseCollection);

        //    return response.ResponseCollection;
        //}

        ///// <summary>
        /////     通过 Http 获取数据权限数据并缓存
        ///// </summary>
        ///// <param name="moduleId"></param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //private DataPermissionInfoCollection GetDataPermissionCollection(string moduleId, string userId, string pubKey,
        //    string tokenId)
        //{
        //    var response = GetWithHttpClient<DataPermissionResponse>(
        //        string.Format("{0}/api/permissions/datas?userid={1}&moduleId={2}", GmpLeaRunWebapi, userId,
        //            moduleId), userId, pubKey, tokenId);
        //    if (response == null || response.ResponseCollection == null || !response.ResponseCollection.Any())
        //        return null;
        //    var dataPermissionInfoCollection = new DataPermissionInfoCollection();
        //    var responseCollection = response.ResponseCollection.Where(e => e.ModuleId == moduleId);
        //    if (!responseCollection.Any())
        //        return null;
        //    foreach (var collection in responseCollection)
        //    {
        //        dataPermissionInfoCollection.Add(collection);
        //    }
        //    var storeId = KeyGeneration.GenerateLeaRunDataAuthorizationStoreKey(userId, moduleId);
        //    RedisClientWrapper.Instance.CacheClient.Set(storeId, dataPermissionInfoCollection);

        //    return dataPermissionInfoCollection;
        //}

        //private ProductInfo GetProduct(string productId, string userId, string pubKey, string tokenId)
        //{
        //    var generateProductInfoStoreKey = KeyGeneration.GenerateProductInfoStoreKey(productId);
        //    var productInfo = RedisClientWrapper.Instance.CacheClient.Get<ProductInfo>(generateProductInfoStoreKey);
        //    if (productInfo != null) return productInfo;

        //    var productResponse =
        //        GetWithHttpClient<ProductResponse>(string.Format("{0}/api/products/{1}", Bdm, productId),
        //            userId, pubKey, tokenId);
        //    if (productResponse == null || productResponse.ProductInfo == null) return null;

        //    RedisClientWrapper.Instance.CacheClient.Set(generateProductInfoStoreKey, productResponse.ProductInfo);
        //    return productResponse.ProductInfo;
        //}

        ///// <summary>
        /////     获取用户缓存
        ///// </summary>
        ///// <param name="queryUserId"></param>
        ///// <param name="userId"></param>
        ///// <param name="pubKey"></param>
        ///// <param name="tokenId"></param>
        ///// <returns></returns>
        //private UserInfo GetCurrentUserCache(string queryUserId, string userId, string pubKey, string tokenId)
        //{
        //    var storeId = KeyGeneration.GenerateUserInfoStoreKey(queryUserId);
        //    var userInfo = RedisClientWrapper.Instance.CacheClient.Get<UserInfo>(storeId);
        //    if (userInfo != null) return userInfo;

        //    var infoCollectionWebResponse = GetWithHttpClient<UserInfoWebResponse>(
        //        string.Format("{0}/api/users/{1}", GmpLeaRunWebapi, queryUserId), userId,
        //        pubKey, tokenId);

        //    if (infoCollectionWebResponse == null) return null;

        //    RedisClientWrapper.Instance.CacheClient.Set(storeId, infoCollectionWebResponse.User);
        //    return infoCollectionWebResponse.User;
        //}

        //#endregion
    }
}