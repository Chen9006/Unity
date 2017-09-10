#include <iostream>
#include <ctime>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <string>
#include <fstream>
#include <direct.h>

using namespace std;

class generator
{
public:
	void generate_sudoku();          //获取随机数
	int get_random_number();      //构建数独棋盘
	void print_sudoku();               //打印数独棋盘
	void generate_txt(string str); //将数独棋盘写入文件
};

