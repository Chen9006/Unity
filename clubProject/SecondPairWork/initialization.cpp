#include "initialization.h"


int voluntary_flag[21] = { 0 };
int stu_freetime_day_flag[10] = { 0 };
int mark_count_stu[301] = { 0 };

int count_stu = 300;

struct student
{
	int sid; // 学生编号
	double gpa; // 学生绩点
	string interest[12];//兴趣标签
	int interest_cnt;//兴趣标签个数
	int voluntary[5]; //学生志愿部门
	int start_freetime[10][2];//学生空闲开始时间
	int end_freetime[10][2];//学生空闲结束时间
	int freetime_cnt;//学生空闲时间个数
	vector<int>admission_dep;//加入的部门编号
	bool mark_freetime[10];//标记某个空闲时间段是否已有部门活动
} stu[301];

struct department
{
	int did; //部门编号
	int num; //纳新人数
	int numrest; //剩余席位
	string feature[12];//特点标签
	int feature_cnt; //特点标签个数
	int start_hdtime[2][2];//部门活动开始时间
	int end_hdtime[2][2];//部门活动结束时间
	int hdtime_cnt;//部门活动时间个数
	vector<struct student> res; //匹配的学生信息
} dep[21];

string bq[12] = { "运动","文学","创新创业","就业服务","交友","文艺汇演","文字新闻采集","监督校风校纪","筹办活动","协助学生会相关工作","营造校园学习氛围","共青团思想建设" };



bool cmp_sid(student s1, student s2)
{
	return (s1.sid < s2.sid);
}

bool cmp_max_gpa(student s1, student s2)
{
	return (s1.gpa > s2.gpa);
}

void initialization::initialize_mark_freetime(int sid)
{
	for (int i = 0; i < 10; i++)
	{
		stu[sid].mark_freetime[i] = false;
	}
}

int initialization::time_is_ok(int sid, int did)
{
	for (int i = 0; i < stu[sid].freetime_cnt; i++)
	{
		for (int j = 0; j < dep[did].hdtime_cnt; j++)
		{
			if (!stu[i].mark_freetime[i] && stu[sid].start_freetime[i][0] == dep[did].start_hdtime[j][0] && stu[sid].start_freetime[i][1] <= dep[did].start_hdtime[j][1] && stu[sid].end_freetime[i][1] >= dep[did].end_hdtime[j][1])
				return i;
		}
	}

	return -1; //表示时间冲突
}

bool initialization::InterestMatching(int sid, int did)
{
	for (int i = 0; i < stu[sid].interest_cnt; i++)
	{
		for (int j = 0; j < dep[did].feature_cnt; j++)
		{
			if (stu[did].interest[i] == dep[did].feature[j])
				return true;
		}
	}

	return false;
}

void initialization::initialize_stu_information(FILE* fp1)
{
	int a;
	for (int i = 1; i < 301; i++)
	{
		initialize_mark_freetime(i);

		fscanf(fp1, "%d", &stu[i].sid);
		//printf("%d\n", stu[i].sid);

		fscanf(fp1, "%lf", &stu[i].gpa);


		fscanf(fp1, "%d", &stu[i].interest_cnt);


		for (int j = 0; j < stu[i].interest_cnt; j++)
		{
			fscanf(fp1, "%d", &a);
			stu[i].interest[j] = bq[a];
		}

		fscanf(fp1, "%d", &a);

		for (int j = 0; j < 5; j++)
		{

			int a1;
			fscanf(fp1, "%d", &stu[i].voluntary[j]);

		}

		fscanf(fp1, "%d", &stu[i].freetime_cnt);

		for (int j = 0; j < stu[i].freetime_cnt; j++)
		{
			fscanf(fp1, "%d %d %d", &stu[i].start_freetime[j][0], &stu[i].start_freetime[j][1], &stu[i].end_freetime[j][1]);
			stu[i].end_freetime[j][0] = stu[i].start_freetime[j][0];
		}
	}

}

void initialization::initialize_dep_information(FILE* fp1)
{
	int a;
	for (int i = 1; i < 21; i++)
	{
		fscanf(fp1, "%d", &dep[i].did);
		//printf("%d\n", dep[i].did);

		fscanf(fp1, "%d", &dep[i].num);
		dep[i].numrest = dep[i].num;
		fscanf(fp1, "%d", &dep[i].feature_cnt);

		for (int j = 0; j < dep[i].feature_cnt; j++)
		{
			fscanf(fp1, "%d", &a);
			dep[i].feature[j] = bq[a];
		}
		fscanf(fp1, "%d", &dep[i].hdtime_cnt);

		for (int j = 0; j < dep[i].hdtime_cnt; j++)
		{
			fscanf(fp1, "%d %d %d", &dep[i].start_hdtime[j][0], &dep[i].start_hdtime[j][1], &dep[i].end_hdtime[j][1]);
			dep[i].end_hdtime[j][0] = dep[i].start_hdtime[j][0];

		}
	}
}


void initialization::GraFirst()
{
	sort(stu, stu + 301, cmp_max_gpa);

	for (int j = 0; j <= 4; j++)
	{
		for (int i = 1; i <= 300; i++)
		{

			int temp = stu[i].voluntary[j];
			if (dep[temp].numrest <= 0) continue;

			int mark = time_is_ok(i, temp);

			if (mark == -1) continue;


			else
			{
				stu[i].mark_freetime[mark] = true;//标记该时间段已有部门活动
				dep[temp].res.push_back(stu[i]); //将学生信息加入部门的匹配vector中
				stu[i].admission_dep.push_back(temp);//将部门编号加入学生匹配部门编号vector中
				if (dep[temp].numrest > 0)
					dep[temp].numrest--; //剩余席位-1
			}

		}
	}
}

void initialization::InterestFirst()
{
	for (int j = 0; j <= 4; j++)
	{
		for (int i = 1; i <= 300; i++)
		{
			int temp = stu[i].voluntary[j];
			if (dep[temp].numrest <= 0) continue;

			int mark = time_is_ok(i, temp);

			if (mark == -1) continue;

			else if (InterestMatching(i, temp))
			{
				stu[i].mark_freetime[mark] = true;//标记该时间段已有部门活动
				dep[temp].res.push_back(stu[i]); //将学生信息加入部门的匹配vector中
				stu[i].admission_dep.push_back(temp);//将部门编号加入学生匹配部门编号vector中
				if (dep[temp].numrest > 0)
					dep[temp].numrest--; //剩余席位-1
			}

		}

	}

}

void initialization::print_output_GF()
{
	freopen("./output_GF.txt", "w", stdout);

	for (int i = 1; i <= 20; i++)
	{
		printf("第%d个部门的纳新情况：\n", dep[i].did);

		if (!dep[i].res.empty())
		{
			printf("预期纳新人数：%d 实际纳新人数：%d\n", dep[i].num, dep[i].res.size());

			vector<struct student>::iterator iter = dep[i].res.begin();
			sort(dep[i].res.begin(), dep[i].res.end(), cmp_max_gpa);
			for (iter; iter != dep[i].res.end(); ++iter)
			{
				cout << "学生编号：" << iter->sid << " " << "学生绩点 : " << iter->gpa << " " << endl;
				if (mark_count_stu[iter->sid] == 0)
				{
					mark_count_stu[iter->sid] = 1;
					count_stu--;
				}
			}
			printf("\n");

		}
		else
		{
			printf("当前无人选择该部门\n");
			printf("\n");
			continue;
		}

		printf("\n");
	}

	printf("\n");
	printf("\n");

	printf("---------------------------------------------\n");

	printf("\n");
	printf("\n");

	sort(stu, stu + 301, cmp_sid);

	for (int i = 1; i <= 300; i++)
	{
		vector<int>::iterator iter = stu[i].admission_dep.begin();
		printf("编号为%d的学生加入部门情况：\n", stu[i].sid);
		if (!stu[i].admission_dep.empty())
		{

			for (iter; iter != stu[i].admission_dep.end(); ++iter)
			{
				cout << "部门编号：" << *iter << " " << endl;
			}
		}
		else
		{
			printf("当前未加入任何部门\n");
		}

		printf("\n");
		printf("\n");

	}

	printf("共计有%d个学生未加入任何部门\n", count_stu);
	fclose(stdout);
}

void initialization::print_output_IF()
{
	freopen("./output_IF.txt", "w", stdout);

	for (int i = 1; i <= 20; i++)
	{
		printf("第%d个部门的纳新情况：\n", dep[i].did);

		if (!dep[i].res.empty())
		{
			printf("预期纳新人数：%d 实际纳新人数：%d\n", dep[i].num, dep[i].res.size());

			vector<struct student>::iterator iter = dep[i].res.begin();
			sort(dep[i].res.begin(), dep[i].res.end(), cmp_sid);
			for (iter; iter != dep[i].res.end(); ++iter)
			{
				cout << "学生编号：" << iter->sid << " ";
				cout << "学生兴趣 : ";
				for (int k = 0; k < iter->interest_cnt; k++)
				{
					cout << iter->interest[k] << " ";
				}
				cout << endl;
				if (mark_count_stu[iter->sid] == 0)
				{
					mark_count_stu[iter->sid] = 1;
					count_stu--;
				}

			}
			printf("\n");
		}
		else
		{
			printf("当前部门未接纳学生\n");
			printf("\n");
			continue;
		}

		printf("\n");
	}

	printf("\n");
	printf("\n");

	printf("---------------------------------------------\n");

	printf("\n");
	printf("\n");

	sort(stu, stu + 301, cmp_sid);

	for (int i = 1; i <= 300; i++)
	{
		vector<int>::iterator iter = stu[i].admission_dep.begin();
		printf("编号为%d的学生加入部门情况：\n", stu[i].sid);
		if (!stu[i].admission_dep.empty())
		{

			for (iter; iter != stu[i].admission_dep.end(); ++iter)
			{
				cout << "部门编号：" << *iter << " " << endl;
			}
		}
		else
		{
			printf("当前未加入任何部门\n");
		}

		printf("\n");
		printf("\n");
	}

	printf("\n");
	printf("\n");
	printf("共计有%d个学生未加入任何部门\n", count_stu);
	fclose(stdout);
}





