using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.ValidateReCAPTCHA;


public record ValidateReCAPTCHACommnad(
   string RecaptchaToken): IRequest<ErrorOr<Unit>>;


