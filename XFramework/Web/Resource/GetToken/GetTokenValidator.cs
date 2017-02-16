using ServiceStack.FluentValidation;

namespace XFramework.Web.Resource.GetToken
{
    public class GetTokenValidator : AbstractValidator<GetTokenRequest>
    {
        public GetTokenValidator()
        {
            RuleFor(r => r.Body.GrantType).NotEmpty().WithMessage("登录类型不能为空!");
            RuleFor(r => r.Body.AuthId).NotEmpty().WithMessage("登录Id不能为空!");
            RuleFor(r => r.Body.Secret).NotEmpty().WithMessage("密码不能为空!");
        }
    }
}
