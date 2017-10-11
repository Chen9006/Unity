#include<iostream>
#include<cstdio>
#include<vector>
#include<string>
#include<cstdlib> 
#include<ctime>
#include "initialization.h"

using namespace std;

int main(int argc, char* argv[])
{
	FILE *fp1;
	fp1 = fopen("import.txt", "r");

	string str;
	str = argv[1];

	/*string str;
	cin >> str;*/

	if (str == "-GF")
	{
		initialization ini;
		ini.initialize_dep_information(fp1);
		ini.initialize_stu_information(fp1);
		fclose(fp1);
		ini.GraFirst();
		ini.print_output_GF();
		
	}

	else if (str == "-IF")
	{
		initialization ini;
		ini.initialize_dep_information(fp1);
		ini.initialize_stu_information(fp1);
		fclose(fp1);
		ini.InterestFirst();
		ini.print_output_IF();
	}

	else
		printf("ERROR :  The parameter  is mismatching !");



	return 0;
}