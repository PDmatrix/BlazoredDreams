import auth0 from 'auth0-js';
import jwt_decode from 'jwt-decode';
import router from 'umi/router';
import { UsersApi } from '@/api';

const AUTH_CONFIG = {
  callbackUrl: process.env.AUTH0_CALLBACK_URL,
  clientId: process.env.AUTH0_CLIENT_ID,
  domain: process.env.AUTH0_DOMAIN,
  audience: process.env.AUTH0_AUDIENCE,
};

const useAuth = () => {
  const auth = new auth0.WebAuth({
    audience: AUTH_CONFIG.audience || '',
    clientID: AUTH_CONFIG.clientId || '',
    domain: AUTH_CONFIG.domain || '',
    redirectUri: AUTH_CONFIG.callbackUrl,
    responseType: 'token id_token',
    scope: 'openid profile email',
  });

  let tokenRenewalTimeout: any;
  const renewToken = () => {
    auth.checkSession({}, (err, result) => {
      if (err) {
        alert(`Could not get a new token (${err.error}: ${err.errorDescription}).`);
      } else {
        setSession(result);
        alert(`Successfully renewed auth!`);
      }
    });
  };

  const scheduleRenewal = () => {
    const exp = localStorage.getItem('expires_at');
    if (!exp) {
      return;
    }
    const expiresAt = JSON.parse(exp);
    const delay = expiresAt - Date.now();
    if (delay > 0) {
      tokenRenewalTimeout = setTimeout(() => {
        renewToken();
      }, delay);
    }
  };

  const setSession = (authResult: any) => {
    // Set the time that the access token will expire at
    const expiresAt = JSON.stringify(authResult.expiresIn * 1000 + new Date().getTime());

    localStorage.setItem('access_token', authResult.accessToken);
    localStorage.setItem('id_token', authResult.idToken);
    localStorage.setItem('expires_at', expiresAt);

    // schedule a token renewal
    scheduleRenewal();

    // navigate to the home route
    router.push('/user');
  };

  return {
    getUserData: (): any => {
      const cookie = localStorage.getItem('id_token');
      if (!cookie) {
        return '';
      }
      return jwt_decode(cookie);
    },
    getUserId: (): string => {
      const cookie = localStorage.getItem('id_token');
      if (!cookie) {
        return '';
      }
      const jwt: { sub: string } = jwt_decode(cookie);
      return jwt.sub;
    },
    getAccessToken: (): string => {
      return 'Bearer ' + localStorage.getItem('access_token') || '';
    },
    isAuthenticated: (): boolean => {
      const exp = localStorage.getItem('expires_at');
      if (!exp) {
        return false;
      }
      const expiresAt = JSON.parse(exp);
      return new Date().getTime() < expiresAt;
    },
    login: (): void => {
      auth.authorize();
    },
    handleAuthentication: async (): Promise<void> => {
      auth.parseHash(async (err, authResult) => {
        if (authResult && authResult.accessToken && authResult.idToken) {
          setSession(authResult);
          const api = new UsersApi({ apiKey: 'Bearer ' + authResult.accessToken });
          await api.usersSignIn();
          router.push(`/user`);
        } else if (err) {
          router.push('/user');
        }
      });
    },
    logout: (): void => {
      // Clear access token and ID token from local storage
      localStorage.removeItem('access_token');
      localStorage.removeItem('id_token');
      localStorage.removeItem('expires_at');
      localStorage.removeItem('scopes');
      clearTimeout(tokenRenewalTimeout);
      auth.logout({ returnTo: window.location.origin });
    },
  };
};

export default useAuth;
