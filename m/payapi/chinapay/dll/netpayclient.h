int __cdecl verifyTransResponse(char MerId[15], char OrdId[16], char TransAmt[12], char CuryId[3], char TransDate[8], char TransType[4], char OrdStat[4], char ChkValue[256]);
int __cdecl verifySignData(char *SignData, char ChkValue[256]);
int __cdecl signOrder(char MerId[15], char OrdId[16], char TransAmt[12], char CuryId[3], char TransDate[8], char TransType[4], char ChkValue[256]);
int __cdecl signData(char MerId[15], char *SignData, char ChkValue[256]);
void __cdecl setMerKeyFile(char keyFile[256]);
void __cdecl unsetMerKeyFile();
void __cdecl setPubKeyFile(char keyFile[256]);
void __cdecl unsetPubKeyFile();
int __cdecl digitalSign(char *MerId, char *SignData, char *keyFile, char *ChkValue)
int __cdecl validateSign(char *MerId, char *PlainData, char *ChkValue, char *keyFile);
