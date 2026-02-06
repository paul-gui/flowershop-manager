export interface RegisterAccountRequest {
  email: string;
  password: string;
  passwordConfirmation: string;
  name: string;
}

export interface LoginRequest{
  email: string;
  password: string;
}

export interface ResetPasswordRequest{
  token: string;
  email: string;
  password: string;
  passwordConfirmation: string;
}