using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayTest.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        public IActionResult PlayCeShi()
        {

            string privateKey = "MIIEowIBAAKCAQEApjBFp7t56sFzJnSWAGTE46rHR1u1DFdaUHfu9hORBxeruMWWBlq+LSwOtd9GLfP2/ARgcQlJjZK8GaIv/HMDg7xjbWQ3ORUiqL31IrPhMsz8IY1L7MPAMs0T0HNfM56BtFZ6+n1czuU5bjftx3N453Hsj9h7zOyc1t+Jz2wkXrKE1fST/ypthpBqLmudlzUKB31Vcic7dIPakPGjhwrKb3Akyr9nwq3HsbVUcr9M9dJXIkycQVm3XWVZIR6xnSzFhgzr2TwdfSeRb0OnR4PNLG1AUqpZZYynKMuG+YyJ+T3MkFKj8usHVtdjIIAlEXKXjtPvIEug3o7TkvKeLaITDQIDAQABAoIBAHnENtVObWcPtSKBHANP0l+KGxdy33Yabwc0yTcuYP4UXvM0etdPF7cbZ+/kl5RGBDrlrhBEoMsFr6MMjiJn1zRoDWXynu/JR6rh/EIgEKpbPYYL9IyXZTUTxCfg0OmOgzSyhMsSnbTOicTzX9rbBcR3WcCP+y3MVKKwCVacww1KiDTz9AVDpc59kYPY7DwEjg4LeMly54IdPW3Wc+7cIOvQkfc7r0TJwIkebIYuM6z9iKu4o/jRYbER2FeeGMSTZG4yudP5+3iv4wKp50Dvchz3Y90ob5DShYpmmbIaiuPhWJ7HjnybOxB6xHep2J/M0uVT223+sViKGyREfHfkWKECgYEA8ZH2zbzmYPTkCeWNyzaE8Yc86EkEGS3/olKjDmKQhoo7tcY47sTL6Z8hUzMjtCDEPNVoDpud/z5YU95z+PRmS2dscL6lxNdeIP5yfjZ1y7f4qR1JETZZi/9CKGerK4fCWj0E3ciDCs4ZtCi4/whzzrUTf4+7+i461aBaXrcAi9UCgYEAsB2W84fKxMtGdCmczp05m+AHlJn8IYH2AaVa1GKXYrGxIdP0Ud5ipDVKOl0tHQ5kKp4tIP6fzAEc+YYgNs706BgjUHSmDhTgglA3kX16Npgmjvapcmak1cxRxQLIoc/LrcdOEN2qLOPPTwvVEntqujtITEiP9ARVyPQKnV2UnlkCgYEA1PtBIxFZ+L/Q0hzuz+zId34pqA7AK0cP7Fh5ZHRoQgZcMJ6MkY82zUo8gnNDFkwRWCU7Mi3ntiOMkp09mJA6JF8SD7E2y/6qAeL0pOx1HOCdvtSQGDnX54NtG9yS4LU4GIFV6ggf6QXfSaThvhEAsY2anEvoiuH5g2F2a61jMOUCgYB7eYNn4oCrUMloj/Q8d8b4Hi2yZfN7mp5bsQ7pcOYqb+J8kRnufDg0YJhWKxWCFaDssFF7nIl0giNKXlsfkiHqx2Ga+u2hWcm+E3eiLZMNy8bc3NsYNhbDPqjls3nu2L04bU+yS3cQWffyt5z/hD/jqcSalxU2RAPRDCiVBBM6oQKBgGX29ky4zAzdgL34qn3Cs+3iMKJ/FLnrI3G04uC9C1QWNdrWMVmCa0/DKvSIFIM0cGaGopVM2vYCxRqvAIt8s8F/P+moPC8JlV4Fp/aCB6ALYa8uKG4G3lL279Jq4VKUcaVBjZ+QKfrtGzsqHaxZF6LR1YyX/Uj2SlvUr52XAkHI";//"MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCmMEWnu3nqwXMmdJYAZMTjqsdHW7UMV1pQd+72E5EHF6u4xZYGWr4tLA6130Yt8/b8BGBxCUmNkrwZoi/8cwODvGNtZDc5FSKovfUis+EyzPwhjUvsw8AyzRPQc18znoG0Vnr6fVzO5TluN+3Hc3jnceyP2HvM7JzW34nPbCResoTV9JP/Km2GkGoua52XNQoHfVVyJzt0g9qQ8aOHCspvcCTKv2fCrcextVRyv0z10lciTJxBWbddZVkhHrGdLMWGDOvZPB19J5FvQ6dHg80sbUBSqllljKcoy4b5jIn5PcyQUqPy6wdW12MggCURcpeO0+8gS6DejtOS8p4tohMNAgMBAAECggEAecQ21U5tZw+1IoEcA0/SX4obF3LfdhpvBzTJNy5g/hRe8zR6108Xtxtn7+SXlEYEOuWuEESgywWvowyOImfXNGgNZfKe78lHquH8QiAQqls9hgv0jJdlNRPEJ+DQ6Y6DNLKEyxKdtM6JxPNf2tsFxHdZwI/7LcxUorAJVpzDDUqINPP0BUOlzn2Rg9jsPASODgt4yXLngh09bdZz7twg69CR9zuvRMnAiR5shi4zrP2Iq7ij+NFhsRHYV54YxJNkbjK50/n7eK/jAqnnQO9yHPdj3ShvkNKFimaZshqK4+FYnseOfJs7EHrEd6nYn8zS5VPbbf6xWIobJER8d+RYoQKBgQDxkfbNvOZg9OQJ5Y3LNoTxhzzoSQQZLf+iUqMOYpCGiju1xjjuxMvpnyFTMyO0IMQ81WgOm53/PlhT3nP49GZLZ2xwvqXE114g/nJ+NnXLt/ipHUkRNlmL/0IoZ6srh8JaPQTdyIMKzhm0KLj/CHPOtRN/j7v6LjrVoFpetwCL1QKBgQCwHZbzh8rEy0Z0KZzOnTmb4AeUmfwhgfYBpVrUYpdisbEh0/RR3mKkNUo6XS0dDmQqni0g/p/MARz5hiA2zvToGCNQdKYOFOCCUDeRfXo2mCaO9qlyZqTVzFHFAsihz8utx04Q3aos489PC9USe2q6O0hMSI/0BFXI9AqdXZSeWQKBgQDU+0EjEVn4v9DSHO7P7Mh3fimoDsArRw/sWHlkdGhCBlwwnoyRjzbNSjyCc0MWTBFYJTsyLee2I4ySnT2YkDokXxIPsTbL/qoB4vSk7HUc4J2+1JAYOdfng20b3JLgtTgYgVXqCB/pBd9JpOG+EQCxjZqcS+iK4fmDYXZrrWMw5QKBgHt5g2figKtQyWiP9Dx3xvgeLbJl83uanluxDulw5ipv4nyRGe58ODRgmFYrFYIVoOywUXuciXSCI0peWx+SIerHYZr67aFZyb4Td6Itkw3Lxtzc2xg2FsM+qOWzee7YvThtT7JLdxBZ9/K3nP+EP+OpxJqXFTZEA9EMKJUEEzqhAoGAZfb2TLjMDN2AvfiqfcKz7eIwon8UuesjcbTi4L0LVBY12tYxWYJrT8Mq9IgUgzRwZoailUza9gLFGq8Ai3yzwX8/6ag8LwmVXgWn9oIHoAthry4obgbeUvbv0mrhUpRxpUGNn5Ap+u0bOyodrFkXotHVjJf9SPZKW9SvnZcCQcg=";// "MIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCDvhutFcwhI84snZGaor488ZYipfEoqOOgo7tRiSN7jO90q15EMQW2ITDWT2ZSumHUmSYHDN9itjdUaOulYJ14W8TFpasmXqGAcVQoPm4ujEfjoK5bz6pwcDH7/oLLmjCxbPTf6PlPfZQU5OnaAkvIpkPJiqslUx1ezhIxfACBlutoomug3DvvQ6ersd3dt5YYptlFuLHhY05P47vm5bkEmFUiE0FEbu0m4BtkAKvc5psG4JnZbpZt8Iqy0QloBCJZsoVPNP61MSWJF3sL9qCv5LM2tJxGReFD8n5UO8QLYTyPJfHPq+uZI7UpgFQ6hatfTAf0LbUrTSYfdIFrfZS7AgMBAAECggEASpi56R7/kABLPR4XlTzzhWSyQ3o8/G3tPkeXyIpjqhDagfCLFwe/GSrgEWmyIcPho1T4oGNclfNhjw096U20vLMRz2yIZdiNkEIbihIXp4nNJwoT0G7dLn+I/gxWGV18luqAGc08ZhNQZyhbdhN4v+vNVHCNijvu1IDQ8m4phzaR3Epym+QS5Xr7I3SEnzB+R81up6c+6qR1kWrcOKtEiVW2OE2iiInfMNicEPplFatUOztliSBx7k1Oc572OoduTO6lAYlMF0HWq0OnLzvibpWC0YV0gAHeRFcv0rv8+3BFidr1rzDi0/p68Xu09VUP/12m+2Nbj2tsB78C4QIaiQKBgQDIgfrQIW7G9rfpjkBXPGNolrwrP5iBRJOFBP4afYgRHkS9qQjYbSjs3HuSZTvFAOToqd60vFTD21lWKw+IiZfFfW4Pnn+aHR4StCfsJqNBpMlJFViRqfMNF0aEVy87U1orlFIq0gXFk9B+k6Es3d1IjGhIt+gb1tbp8VDZQuQxdwKBgQCoNBpmlxB44q2cSAKaU+QrPiIShadkPEpy43TclYb74s54+VlBNRBfka1iidIq2DktCYQpqxaBAV+hvhKTpLgI3FDrRMjrJbeLIrxlJXKYu61S8aPbDjBxvm+c2Bu4RluRQFyWJ5Tm4sLOMjXIhRAMiH9iQ0WHwr/1D7rgbihn3QKBgAGAj8t9ZKIS+R/D1BGTPz9qWbCNQmw9gklt1yWFqT5Plr5BBPLyJmtX34kiPwdj0VM46pU6D/yDqTCvE+e2+C8fmHJ/gn86Fp6XQPFQ/MoiHOvyP/6fYlXHxdx59r2gCz3mV5NAjZCEYfyAyKVYaC8B9koNLqz+uA96oltuV91TAoGABfjrow7kF9a8oVrxNLeb12k6Xnv+aJG3dwLaiGaKx3DTMfpwj98peKs8g84u9zR8OzwmRDB9+RCqa5zLHhDwM+hWuzPTCn5pWPhfxhuhSQkI63QHBGUplTBU2DXYLh/ZanvljbNUFyqgDiA8RqVq33cNRtjqpBpnJHVthkCDEMUCgYBxH2OvBCiwcndFkgG0JxPm4qn3JZ0yScaHmqGI6J3wx7/HOgVVh+4v2xNXOV/Ef5iUP0UK3bl/vRcK/T0HhgB8RYEkFV3xGgYHdV08/1RtibvOHUPGWnamSGPe6PKBhI7pUZ3ugLPqLZwf0WxwnlOd6AmDDOsbs1sitIcpJEPn3g==";
            string alipayPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAn/YhcBvOmvHIsVFRanFxsjUZ46we8PWL0uk8TDBUT3dyCSphrW/HZjPY8v6IYvyfOwIsYa1/aFk4Yj4sxC8gt9f5CjK1bb9zyTPpkh23nkassoKacU6BSxX61W1uT46jpoox+NtILh23i4Dxc1u3I/Ak1pKjKWQqPE/mWobPxIFjTIc3pc5xUcrKRug/Jb2u9ODNIgnzjOxvjA98uU4rpruvIu91PL5MhQAiZl7qLiImPgpvs/2H9idqHzcB49DRZspKn8vCxzuMg50jHCy3TgmDfH4zQvzxPv3ZaBU3CfBtULvi9Hg5bV//mfpF80t1HHzVbw1zsBdWSmTQq+bYgQIDAQAB";// "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAn/YhcBvOmvHIsVFRanFxsjUZ46we8PWL0uk8TDBUT3dyCSphrW/HZjPY8v6IYvyfOwIsYa1/aFk4Yj4sxC8gt9f5CjK1bb9zyTPpkh23nkassoKacU6BSxX61W1uT46jpoox+NtILh23i4Dxc1u3I/Ak1pKjKWQqPE/mWobPxIFjTIc3pc5xUcrKRug/Jb2u9ODNIgnzjOxvjA98uU4rpruvIu91PL5MhQAiZl7qLiImPgpvs/2H9idqHzcB49DRZspKn8vCxzuMg50jHCy3TgmDfH4zQvzxPv3ZaBU3CfBtULvi9Hg5bV//mfpF80t1HHzVbw1zsBdWSmTQq+bYgQIDAQAB";//"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAyZ1k/CE0TYd6AwnBmhIIX5KZigfSnxTbpGO+SMf968ljeoTGrJj0ti40QXhTRyo0p5POTQxeA/IPiihrDdoTAaOV2L23feDLIm+grbP0ZxSYqn6WkOOeyM0rN3q+gjcdorERuE7M+5ToyVVdsH9F7vZLwBnSLzBvYevxFi3SvRf6JEgVUIJ1dl9xFq/M98PYI4mKolh8biQRt7ID5fBrKCivP/j3xNr8WkErNuWPvA6TSeE8/lMW1o2eI6PfwSorE2Ekzck0nKZ/zJZKO8j7xNaSlAq0+RngfmTCyQEWZ0vPdAD8EBQZ3gkIl0ca9rqN20rC5zkLEeohD6iemZBYgwIDAQAB";
            AlipayConfig alipayConfig = new AlipayConfig();
            alipayConfig.ServerUrl = "https://openapi-sandbox.dl.alipaydev.com/gateway.do";
            alipayConfig.AppId = "2021004105694172";
            alipayConfig.PrivateKey = privateKey;
            alipayConfig.Format = "json";
            alipayConfig.AlipayPublicKey = alipayPublicKey;
            alipayConfig.Charset = "UTF-8";
            alipayConfig.SignType = "RSA2";
            IAopClient alipayClient = new DefaultAopClient(alipayConfig);
            AlipayTradePagePayRequest request = new AlipayTradePagePayRequest();
            AlipayTradePagePayModel model = new AlipayTradePagePayModel();
            model.OutTradeNo = "20150320010101002";
            model.TotalAmount = "88.88";
            model.Subject = "Iphone6 16G";
            model.ProductCode = "FAST_INSTANT_TRADE_PAY";
            request.SetBizModel(model);
            AlipayTradePagePayResponse response = alipayClient.pageExecute(request);
            if (!response.IsError)
                return Ok("调用成功");
            else
                return Ok("调用失败");
        }
        public string Dbzz() 
        {
            string privateKey = "MIIEowIBAAKCAQEApjBFp7t56sFzJnSWAGTE46rHR1u1DFdaUHfu9hORBxeruMWWBlq+LSwOtd9GLfP2/ARgcQlJjZK8GaIv/HMDg7xjbWQ3ORUiqL31IrPhMsz8IY1L7MPAMs0T0HNfM56BtFZ6+n1czuU5bjftx3N453Hsj9h7zOyc1t+Jz2wkXrKE1fST/ypthpBqLmudlzUKB31Vcic7dIPakPGjhwrKb3Akyr9nwq3HsbVUcr9M9dJXIkycQVm3XWVZIR6xnSzFhgzr2TwdfSeRb0OnR4PNLG1AUqpZZYynKMuG+YyJ+T3MkFKj8usHVtdjIIAlEXKXjtPvIEug3o7TkvKeLaITDQIDAQABAoIBAHnENtVObWcPtSKBHANP0l+KGxdy33Yabwc0yTcuYP4UXvM0etdPF7cbZ+/kl5RGBDrlrhBEoMsFr6MMjiJn1zRoDWXynu/JR6rh/EIgEKpbPYYL9IyXZTUTxCfg0OmOgzSyhMsSnbTOicTzX9rbBcR3WcCP+y3MVKKwCVacww1KiDTz9AVDpc59kYPY7DwEjg4LeMly54IdPW3Wc+7cIOvQkfc7r0TJwIkebIYuM6z9iKu4o/jRYbER2FeeGMSTZG4yudP5+3iv4wKp50Dvchz3Y90ob5DShYpmmbIaiuPhWJ7HjnybOxB6xHep2J/M0uVT223+sViKGyREfHfkWKECgYEA8ZH2zbzmYPTkCeWNyzaE8Yc86EkEGS3/olKjDmKQhoo7tcY47sTL6Z8hUzMjtCDEPNVoDpud/z5YU95z+PRmS2dscL6lxNdeIP5yfjZ1y7f4qR1JETZZi/9CKGerK4fCWj0E3ciDCs4ZtCi4/whzzrUTf4+7+i461aBaXrcAi9UCgYEAsB2W84fKxMtGdCmczp05m+AHlJn8IYH2AaVa1GKXYrGxIdP0Ud5ipDVKOl0tHQ5kKp4tIP6fzAEc+YYgNs706BgjUHSmDhTgglA3kX16Npgmjvapcmak1cxRxQLIoc/LrcdOEN2qLOPPTwvVEntqujtITEiP9ARVyPQKnV2UnlkCgYEA1PtBIxFZ+L/Q0hzuz+zId34pqA7AK0cP7Fh5ZHRoQgZcMJ6MkY82zUo8gnNDFkwRWCU7Mi3ntiOMkp09mJA6JF8SD7E2y/6qAeL0pOx1HOCdvtSQGDnX54NtG9yS4LU4GIFV6ggf6QXfSaThvhEAsY2anEvoiuH5g2F2a61jMOUCgYB7eYNn4oCrUMloj/Q8d8b4Hi2yZfN7mp5bsQ7pcOYqb+J8kRnufDg0YJhWKxWCFaDssFF7nIl0giNKXlsfkiHqx2Ga+u2hWcm+E3eiLZMNy8bc3NsYNhbDPqjls3nu2L04bU+yS3cQWffyt5z/hD/jqcSalxU2RAPRDCiVBBM6oQKBgGX29ky4zAzdgL34qn3Cs+3iMKJ/FLnrI3G04uC9C1QWNdrWMVmCa0/DKvSIFIM0cGaGopVM2vYCxRqvAIt8s8F/P+moPC8JlV4Fp/aCB6ALYa8uKG4G3lL279Jq4VKUcaVBjZ+QKfrtGzsqHaxZF6LR1YyX/Uj2SlvUr52XAkHI";//"MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCmMEWnu3nqwXMmdJYAZMTjqsdHW7UMV1pQd+72E5EHF6u4xZYGWr4tLA6130Yt8/b8BGBxCUmNkrwZoi/8cwODvGNtZDc5FSKovfUis+EyzPwhjUvsw8AyzRPQc18znoG0Vnr6fVzO5TluN+3Hc3jnceyP2HvM7JzW34nPbCResoTV9JP/Km2GkGoua52XNQoHfVVyJzt0g9qQ8aOHCspvcCTKv2fCrcextVRyv0z10lciTJxBWbddZVkhHrGdLMWGDOvZPB19J5FvQ6dHg80sbUBSqllljKcoy4b5jIn5PcyQUqPy6wdW12MggCURcpeO0+8gS6DejtOS8p4tohMNAgMBAAECggEAecQ21U5tZw+1IoEcA0/SX4obF3LfdhpvBzTJNy5g/hRe8zR6108Xtxtn7+SXlEYEOuWuEESgywWvowyOImfXNGgNZfKe78lHquH8QiAQqls9hgv0jJdlNRPEJ+DQ6Y6DNLKEyxKdtM6JxPNf2tsFxHdZwI/7LcxUorAJVpzDDUqINPP0BUOlzn2Rg9jsPASODgt4yXLngh09bdZz7twg69CR9zuvRMnAiR5shi4zrP2Iq7ij+NFhsRHYV54YxJNkbjK50/n7eK/jAqnnQO9yHPdj3ShvkNKFimaZshqK4+FYnseOfJs7EHrEd6nYn8zS5VPbbf6xWIobJER8d+RYoQKBgQDxkfbNvOZg9OQJ5Y3LNoTxhzzoSQQZLf+iUqMOYpCGiju1xjjuxMvpnyFTMyO0IMQ81WgOm53/PlhT3nP49GZLZ2xwvqXE114g/nJ+NnXLt/ipHUkRNlmL/0IoZ6srh8JaPQTdyIMKzhm0KLj/CHPOtRN/j7v6LjrVoFpetwCL1QKBgQCwHZbzh8rEy0Z0KZzOnTmb4AeUmfwhgfYBpVrUYpdisbEh0/RR3mKkNUo6XS0dDmQqni0g/p/MARz5hiA2zvToGCNQdKYOFOCCUDeRfXo2mCaO9qlyZqTVzFHFAsihz8utx04Q3aos489PC9USe2q6O0hMSI/0BFXI9AqdXZSeWQKBgQDU+0EjEVn4v9DSHO7P7Mh3fimoDsArRw/sWHlkdGhCBlwwnoyRjzbNSjyCc0MWTBFYJTsyLee2I4ySnT2YkDokXxIPsTbL/qoB4vSk7HUc4J2+1JAYOdfng20b3JLgtTgYgVXqCB/pBd9JpOG+EQCxjZqcS+iK4fmDYXZrrWMw5QKBgHt5g2figKtQyWiP9Dx3xvgeLbJl83uanluxDulw5ipv4nyRGe58ODRgmFYrFYIVoOywUXuciXSCI0peWx+SIerHYZr67aFZyb4Td6Itkw3Lxtzc2xg2FsM+qOWzee7YvThtT7JLdxBZ9/K3nP+EP+OpxJqXFTZEA9EMKJUEEzqhAoGAZfb2TLjMDN2AvfiqfcKz7eIwon8UuesjcbTi4L0LVBY12tYxWYJrT8Mq9IgUgzRwZoailUza9gLFGq8Ai3yzwX8/6ag8LwmVXgWn9oIHoAthry4obgbeUvbv0mrhUpRxpUGNn5Ap+u0bOyodrFkXotHVjJf9SPZKW9SvnZcCQcg=";// "MIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCDvhutFcwhI84snZGaor488ZYipfEoqOOgo7tRiSN7jO90q15EMQW2ITDWT2ZSumHUmSYHDN9itjdUaOulYJ14W8TFpasmXqGAcVQoPm4ujEfjoK5bz6pwcDH7/oLLmjCxbPTf6PlPfZQU5OnaAkvIpkPJiqslUx1ezhIxfACBlutoomug3DvvQ6ersd3dt5YYptlFuLHhY05P47vm5bkEmFUiE0FEbu0m4BtkAKvc5psG4JnZbpZt8Iqy0QloBCJZsoVPNP61MSWJF3sL9qCv5LM2tJxGReFD8n5UO8QLYTyPJfHPq+uZI7UpgFQ6hatfTAf0LbUrTSYfdIFrfZS7AgMBAAECggEASpi56R7/kABLPR4XlTzzhWSyQ3o8/G3tPkeXyIpjqhDagfCLFwe/GSrgEWmyIcPho1T4oGNclfNhjw096U20vLMRz2yIZdiNkEIbihIXp4nNJwoT0G7dLn+I/gxWGV18luqAGc08ZhNQZyhbdhN4v+vNVHCNijvu1IDQ8m4phzaR3Epym+QS5Xr7I3SEnzB+R81up6c+6qR1kWrcOKtEiVW2OE2iiInfMNicEPplFatUOztliSBx7k1Oc572OoduTO6lAYlMF0HWq0OnLzvibpWC0YV0gAHeRFcv0rv8+3BFidr1rzDi0/p68Xu09VUP/12m+2Nbj2tsB78C4QIaiQKBgQDIgfrQIW7G9rfpjkBXPGNolrwrP5iBRJOFBP4afYgRHkS9qQjYbSjs3HuSZTvFAOToqd60vFTD21lWKw+IiZfFfW4Pnn+aHR4StCfsJqNBpMlJFViRqfMNF0aEVy87U1orlFIq0gXFk9B+k6Es3d1IjGhIt+gb1tbp8VDZQuQxdwKBgQCoNBpmlxB44q2cSAKaU+QrPiIShadkPEpy43TclYb74s54+VlBNRBfka1iidIq2DktCYQpqxaBAV+hvhKTpLgI3FDrRMjrJbeLIrxlJXKYu61S8aPbDjBxvm+c2Bu4RluRQFyWJ5Tm4sLOMjXIhRAMiH9iQ0WHwr/1D7rgbihn3QKBgAGAj8t9ZKIS+R/D1BGTPz9qWbCNQmw9gklt1yWFqT5Plr5BBPLyJmtX34kiPwdj0VM46pU6D/yDqTCvE+e2+C8fmHJ/gn86Fp6XQPFQ/MoiHOvyP/6fYlXHxdx59r2gCz3mV5NAjZCEYfyAyKVYaC8B9koNLqz+uA96oltuV91TAoGABfjrow7kF9a8oVrxNLeb12k6Xnv+aJG3dwLaiGaKx3DTMfpwj98peKs8g84u9zR8OzwmRDB9+RCqa5zLHhDwM+hWuzPTCn5pWPhfxhuhSQkI63QHBGUplTBU2DXYLh/ZanvljbNUFyqgDiA8RqVq33cNRtjqpBpnJHVthkCDEMUCgYBxH2OvBCiwcndFkgG0JxPm4qn3JZ0yScaHmqGI6J3wx7/HOgVVh+4v2xNXOV/Ef5iUP0UK3bl/vRcK/T0HhgB8RYEkFV3xGgYHdV08/1RtibvOHUPGWnamSGPe6PKBhI7pUZ3ugLPqLZwf0WxwnlOd6AmDDOsbs1sitIcpJEPn3g==";
            AlipayConfig alipayConfig = new AlipayConfig();
            alipayConfig.PrivateKey = privateKey;
            alipayConfig.ServerUrl = "https://openapi.alipay.com/gateway.do";
            alipayConfig.AppId = "2021004105694172";
            alipayConfig.Charset = "UTF-8";
            alipayConfig.SignType = "RSA2";
            alipayConfig.EncryptKey = "";
            alipayConfig.Format = "json";
            alipayConfig.AppCertPath = "<-- 请填写您的应用公钥证书文件路径，例如：/foo/appCertPublicKey_2019051064521003.crt -->";
            alipayConfig.AlipayPublicCertPath = "<-- 请填写您的支付宝公钥证书文件路径，例如：/foo/alipayCertPublicKey_RSA2.crt -->";
            alipayConfig.RootCertPath = "<-- 请填写您的支付宝根证书文件路径，例如：/foo/alipayRootCert.crt -->";
            IAopClient alipayClient = new DefaultAopClient(alipayConfig);
            AlipayFundTransUniTransferRequest request = new AlipayFundTransUniTransferRequest();
            AlipayFundTransUniTransferModel model = new AlipayFundTransUniTransferModel();
            model.OutBizNo = "201806300001";
            model.Remark = "201905代发";
            model.BusinessParams = "{\"payer_show_name_use_alias\":\"true\"}";
            model.BizScene = "DIRECT_TRANSFER";
            Participant payeeInfo = new Participant();
            payeeInfo.Identity = "2088123412341234";
            payeeInfo.IdentityType = "ALIPAY_USER_ID";
            payeeInfo.Name = "黄龙国际有限公司";
            model.PayeeInfo = payeeInfo;
            model.TransAmount = "23.00";
            model.ProductCode = "TRANS_ACCOUNT_NO_PWD";
            model.OrderTitle = "201905代发";
            request.SetBizModel(model);
            AlipayFundTransUniTransferResponse response = alipayClient.CertificateExecute(request);
            if (!response.IsError)
            {
                return "调用成功";
            }
            else
            {
                return "调用失败";
            }

        }
    }
}
