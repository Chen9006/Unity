#include<iostream>
#include<cstdio>
#include<vector>
#include<cstring>
#include<cstdlib>
#include<ctime>
using namespace std;

string bq[12] = { "sports","lecture","cxcy","jyfw","jy","wyhy","wxjwcj","jdxfxj","cbhd","xzxxgz","yzxyxxqf","gqtsxjs"};

int main() {
	srand(time(NULL));
	FILE *fp1;
	fp1=fopen("import.txt","w");
	int i,j,cnt,test[21];
	for(i=1; i<=20; i++) {
		fprintf(fp1,"%d ",i);
		fprintf(fp1,"%d ",rand()%4+12);
		cnt=rand()%3+5;
		fprintf(fp1,"%d ",cnt);
		int l=rand()%12;
		for(j=0; j<cnt; j++) {
			l=(l+1)%12;
			fprintf(fp1,"%d ",l);
		}
		//fprintf(fp1,"\n");
		cnt=rand()%2+1;
		fprintf(fp1,"%d ",cnt);
		memset(test,0,sizeof(test));
		for(j=0; j<cnt;) {

			int day=rand()%7+1;
			if(test[day]==0) {
				int hour=rand() % 2 + 38;
				test[day]=1;
				j++;
				fprintf(fp1,"%d %d %d\n",day,hour,hour+3);
			}
		}
		fprintf(fp1,"\n");
	}

	for(i=1; i<=300; i++) {
		fprintf(fp1,"%d\n",i);
		fprintf(fp1,"%.2lf\n",(1 + rand() % 4 + double(rand() % 10) / 10));
		cnt=rand()%3+5;
		fprintf(fp1,"%d\n",cnt);
		int l=rand()%10;
		for(j=0; j<cnt; j++) {
			l=(l+1)%10;
			fprintf(fp1,"%d ",l);
		}
        fprintf(fp1,"\n");
		cnt=5;
		fprintf(fp1,"%d\n",cnt);
		memset(test,0,sizeof(test));
		for(j=0; j<cnt;) {

			int l=rand()%20+1;
			if(test[l]==0) {
				fprintf(fp1,"%d ",l);
				test[l]=1;
				j++;
			}
		}
	    fprintf(fp1,"\n");

		cnt=rand()%2+5;
		fprintf(fp1,"%d\n",cnt);
		memset(test,0,sizeof(test));
		for(j=0; j<cnt;) {

			int day=rand()%7+1;
			if(test[day]==0) {
				int hour=rand() % 3 + 36;
				test[day]=1;
				j++;
				fprintf(fp1,"%d %d %d\n",day,hour,rand()%3+hour+4);
			}
		}
		fprintf(fp1,"\n");
	}

	fclose(fp1);
	return 0;
}
/*
提供输入包括：20个部门（包含各部门需要学生数的要求的上限，单个，数值，在[0,15]内；
各部门的特点标签，多个，字符；各部门的常规活动时间段，多个，字符/日期），
300个学生（包含绩点信息，单个，数值；兴趣标签，多个，字符），
每个学生有不多于5个的部门意愿（部门意愿不能空缺）和空闲时间段（多个，字符/日期）。
实现一个智能自动分配算法，根据输入信息，输出部门和学生间的匹配信息
（一个学生可以确认多个他所申请的部门，一个部门可以分配少于等于其要求的学生数的学生）
 及 未被分配到学生的部门 和 未被部门选中的学生。*/


