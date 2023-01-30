import axios from 'axios'
import Constants from "../Constants";

export default async function RefreshToken() {

    let accessToken = sessionStorage.getItem('AccessToken');
    let tokenType = Constants.TOKEN_TYPE;
    let tokenTypeWithSpaceLength = (`${tokenType}` + ` `).length;
    let accessTokenLenght = accessToken.length;
    let accessTokenWithoutTokenType = accessTokenWithBearer.substring(tokenTypeWithSpaceLength, accessTokenLenght);

    let TokenModel = {
      AccessToken: accessTokenWithoutTokenType,
      RefreshToken: sessionStorage.getItem('RefreshToken'),
    }

    const refreshTokenUrl = Constants.API_URL_REFRESH_TOKEN;
    
    let response = await axios.post(refreshTokenUrl, TokenModel);

    if (response.status === 200){
      try{
        let responseFromServer2 = response.data;
      console.log('we are in settingItems');
      console.log(responseFromServer2.accessToken);
      
      sessionStorage.setItem('AccessToken', `${tokenType} ${responseFromServer2.accessToken}`);
      sessionStorage.setItem('RefreshToken', responseFromServer2.refreshToken);
      sessionStorage.setItem('ExpiryTime', responseFromServer2.expiryTime);
      }
      catch{
        console.log(error);
        console.log("ERROR! We're in RefreshToken func");
      }      
    }
}
