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