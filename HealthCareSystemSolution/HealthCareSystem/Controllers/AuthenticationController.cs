using ApplicationLayer.Authentication.Commands;
using ApplicationLayer.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Shared.Dtos;
using System.Security.Claims;

namespace HealthCareSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManager manager, IMediator mediator) : ControllerBase
    {


        //Don't forget to add Login endpoint in the future

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            await mediator.Send(new CreateRegisterCommand(
            dto.UserDto,
            dto.RegistrationToken)
                );

            return NoContent();
        }
        [HttpPost("SendVerificationCode")]
        public async Task<IActionResult> SendVerificationCode([FromBody] string Email)
        {
            try
            {
                await manager.EmailService.SendOTPCode(Email);

                return Ok();
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException($"Failed to send OTP code: {ex.Message}");
            }
        }
        [HttpPost("VerifyRegistrationCode")]
        public async Task<IActionResult> VerifyRegistrationCode([FromBody] VerifyOTPCodeDto dto)
        {
            try
            {
                var isValid = await manager.OTPService.VerifyOTPCodeRegistrationAsync(dto.Email, dto.OTPCode);
                if (isValid is not null)
                {
                    return Ok(isValid);
                }
                else
                {
                    return BadRequest("Invalid OTP code.");
                }
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException($"Failed to verify OTP code: {ex.Message}");
            }
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] string Email)
        {
            try
            {
                await manager.ForgotPasswordService.SendOTPCodeAsync(Email);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException($"Failed to send OTP code: {ex.Message}");
            }
        }
        [HttpPost("VerifyResetPasswordCode")]
        public async Task<IActionResult> VerifyResetPasswordCode([FromBody] VerifyOTPCodeDto dto)
        {
            try
            {
                var isValid = await manager.OTPService.VerifyResetPasswordOTPCodeAsync(dto.Email, dto.OTPCode);
                if (isValid is not null)
                {
                    return Ok(isValid);
                }
                else
                {
                    return BadRequest("Invalid OTP code.");
                }
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException($"Failed to verify OTP code: {ex.Message}");
            }
        }
        [HttpPost("ResetForgivePassword")]
        public async Task<IActionResult> ResetForgivePassword([FromBody] ForgetPasswordRequestDto dto)
        {
            

            return NoContent();
        }
    }
}
