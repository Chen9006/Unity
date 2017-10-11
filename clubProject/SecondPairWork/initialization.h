#include<iostream>
#include<cstdio>
#include<vector>
#include<string>
#include<cstring>
#include<cstdlib> 
#include<ctime>
#include <fstream>
#include <algorithm>

using namespace std;

class initialization
{
public :
	void initialize_stu_information(FILE* fp1); //初始化学生信息
	void initialize_dep_information(FILE* fp1);//初始化部门信息
	void initialize_mark_freetime(int sid); 
	void GraFirst(); //以绩点优先原则纳新
	void InterestFirst(); //以兴趣优先原则纳新
	int  time_is_ok(int sid, int did);//判断时间是否冲突
	bool InterestMatching(int sid, int did);//判断兴趣是否匹配
	void print_output_GF();//以绩点优先原则纳新输出结果
	void print_output_IF();//以兴趣优先原则纳新输出结果
};

