using NLua;
using NPinyin;
using System;
using System.Text;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "";
            for (int i = 1; i < 100; i++)
            {
                str += "S" + i+",";
            }
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//注册编码对象
            Encoding gb2312 = Encoding.GetEncoding("GB2312");
            string strA = Pinyin.ConvertEncoding("好的", Encoding.UTF8, gb2312);
            //首字母
            string strB = Pinyin.GetInitials("好的");
            //拼音 
            string strC = Pinyin.GetPinyin("1");

            Lua lua = new Lua();
            string aa = "string.find('09.8100','81.0',1,true)==nil2";
            var ff5 = lua.DoString(@$"return {aa}")[0];
            var ff3 = lua.DoString(@$"return '{Pinyin.GetPinyin("1")}'=='{Pinyin.GetPinyin("1")}' ")[0];
            var result6 = (bool)lua.DoString("return string.find('A18.808+I32.0*','A18.808+I32.0*')~=nil2")[0];
            result6 = (bool)lua.DoString("return string.sub('A18.808+I32.0*',1,string.len('A18.808+I32.0*'))=='A18.808+I32.0*'")[0];
            var ff2 = lua.DoString(@"return ((os.time(20220104)-os.time(20220101))/(3600*24))")[0];

            var scriptFunc2 = lua["ScriptFunc2"] as LuaFunction;
            //var res = (int)scriptFunc.Call("S1,2,23,4,5", "2").First();
            var res2 = scriptFunc2.Call("20220104", "20220101")[0];



            string aA = "1";
            string a2 = "{aA}aa";
            string a3 = $"" + a2;
            var newstr = "";
            bool strjia = true;
            var pjzd = "";
            foreach (var item in a2)
            {
                if (item == '{')
                {
                    strjia = false;
                }
                else if (strjia == false && item != '}')
                {
                    pjzd += item;
                }
                else if (item == '}')
                {
                    strjia = true;
                    string var = "";


                    pjzd = "";
                }
                if (strjia)
                    newstr += item;
            }

            //string.find("abc","a")~=nil2//判断包含
            //string.sub(String,1,string.len(Start))==Start判断开头
            var asd = string.Compare("c1", "a3");
            var asgval = lua.DoString("return 2011-06-23-2011-06-22");
            var result3 = (bool)lua.DoString("return (string.sub('O81.001',1,string.len('S'))=='S' or string.sub('O81.001',1,string.len('T'))=='T' or string.sub('O08.103',1,string.len('S'))=='S' or string.sub('O08.103',1,string.len('T'))=='T' or string.sub('Z37.001',1,string.len('S'))=='S' or string.sub('Z37.001',1,string.len('T'))=='T' or string.sub('D29.001',1,string.len('S'))=='S' or string.sub('D29.001',1,string.len('T'))=='T' ) and ''=='' ")[0];

            var result2 = lua.DoString("return string.sub('aabb',1,string.len('b'))=='aa'");
            var result = (bool)lua.DoString("return '1'>'1' or \"c1\"<\"a3\"")[0];
            var b = 5;

            var ff = lua.DoString(@"
            	function ScriptFunc (val1, val2)
              a =0;


            local nFindStartIndex = 1  
            local nSplitIndex = 1  
            local nSplitArray = {}  
            while true do  
               local nFindLastIndex = string.find(val1, ',', nFindStartIndex)  
               if not nFindLastIndex then  
                nSplitArray[nSplitIndex] = string.sub(val1, nFindStartIndex, string.len(val1))  
                break  
               end  
               nSplitArray[nSplitIndex] = string.sub(val1, nFindStartIndex, nFindLastIndex - 1)  
               nFindStartIndex = nFindLastIndex + string.len(',')  
               nSplitIndex = nSplitIndex + 1  
            end 

            for i, v in ipairs(nSplitArray) do 

            if string.find(v,val2)~=nil2 then
            			a=a + 1
            		else
            			a=a
            		end
            end


            return a

                                    end
            	");

            var scriptFunc = lua["ScriptFunc"] as LuaFunction;
            //var res = (int)scriptFunc.Call("S1,2,23,4,5", "2").First();
            var res = scriptFunc.Call("S1,2,23,S,5", "S")[0];
            res = scriptFunc.Call("S1,2,23,S,5", "S")[0];
            //var aa5 = "S1,2,23,4,5".Split(',').Where(u => u.Contains("S")).Count() > 1;//.ToList()
            lua.LoadCLRPackage();
            lua.DoString(@" import ('MyAssembly', 'MyNamespace') 
			   import ('System.Linq') ");
            var res0 = lua.DoString("return \"S1, 2, 23, 4, 5\".Split(',').Where(u => u.Contains(\"2\")).Count()>1")[0];
            var c = 0;

        }
    }
}
