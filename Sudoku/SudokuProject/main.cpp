#include <iostream>
#include <ctime>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <string>
#include <sstream>
#include <ctype.h>
#include <shlwapi.h>
#include "generator.h"

using namespace std;


char szFilePath[10000 + 1] = { 0 };


int main(int argc, char* argv[])
{
	string temp = argv[1];
	string str = argv[2]; 

	/*
	参数名字不匹配，报错
	*/

	if (temp != "-c")
	{
		cout << "Error : The name of the parameter is mismatching !" << endl;
		return 0;
	}

	/*
	 参数含非法字符，报错
	*/

	for (int i = 0; i<str.size(); i++)
	{
		if (isdigit(str[i])) continue;
		else
		{
			cout << "Error : The parameter  is not a number !" << endl;
			return 0;
		}
	 }

	stringstream stream;
	stream << str;

	int num;
	stream >> num;

	/*int num;
	cin >> num;
  */

  /*
	获取.exe所在路径
  */

	GetModuleFileNameA(NULL, szFilePath, MAX_PATH);
	(strrchr(szFilePath, '\\'))[0] = 0; // 删除文件名，只获得路径字符串
	string path = szFilePath;

	//cout << path << endl;


	generator ge;  //实例化对象

	for (int i = 0; i < num; i++)
	{
		ge.generate_sudoku();
		//ge.print_sudoku();
		ge.generate_txt(path);
	}

	return 0;
}