 using Newtonsoft.Json;
 namespace Application.ValidateReCAPTCHA;
 public class RecaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            public string ChallengeTs { get; set; }

            public string Hostname { get; set; }
        }