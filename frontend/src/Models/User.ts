export type UserProfileToken = {
  userName: string;
  email: string;
  token: string;
  roles?: string[];
};

export type UserProfile = {
  userName: string;
  email: string;
};

export interface AppUser {
  id: string;
  userName: string;
  email: string;
  emailConfirmed: boolean;
  roles?: string[];
}