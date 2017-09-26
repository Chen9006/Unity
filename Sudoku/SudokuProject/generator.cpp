#include "generator.h"

/*新增要求棋盘左上角的数字确定为（学号后两位和）%9+1*/

int begin_board[3][3];  //数独棋盘中心3*3棋盘(基点棋盘)

int sudoku_board[10][10];
//数独棋盘中心9*9棋盘(9*9棋盘分成9个3*3棋盘填充，从左往右，从上到下记为M1,M2, ... ,M8，M9棋盘)

int number[8] = { 1,2,3,4,5,6,7,9 };

char res[216];

int flag[10]; //判断随机数是否重复

int  unsigned seed = time(NULL);

int q_flag = 0;

int  generator::get_random_number() //获取随机数
{
	srand(seed);
	seed = rand();
	return (rand() % 9 + 1);
}

void generator::generate_sudoku() //构建数独棋盘
{

	//memset(begin_board, 0, sizeof(begin_board));
	memset(flag, 0, sizeof(flag));
	memset(sudoku_board, 0, sizeof(sudoku_board));


	/*
	生成基点3*3棋盘
	*/

	int count = 0;

	if (q_flag == 0)
	{
		if (next_permutation(number, number + 8))
		{

			for (int i = 0; i < 2; i++)
				for (int j = 0; j < 3; j++)
				{
					begin_board[i][j] = number[count];
					count++;
				}


			begin_board[2][0] = number[count];
			count++;
			begin_board[2][1] = number[count];
			count++;
			begin_board[2][2] = 8;
		}
		else
		{
			int count = 0;
			int num = 0;

			flag[8] = 1;

			for (int i = 0; i < 3; i++)
				for (int j = 0; ; j++)
				{
					int temp = get_random_number();
					//cout << temp << endl;
					if (flag[temp] == 0)
					{
						begin_board[i][count] = temp;
						flag[temp] = 1;
						count++;
						num++;

						if (count == 3)
						{
							count = 0;
							break;
						}

						if (num == 8) break;

					}
				}

			begin_board[2][2] = 8;
		}

		q_flag = 1;
	}
	else
	{
		q_flag = 0;
	}

	if (q_flag == 0)
	{
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++)
			{
				sudoku_board[i + 3][j + 3] = begin_board[i][j];  // 生成9*9棋盘的中心的3*3棋盘(M5)

				 /*
		        以基点棋盘进行行变换，向左，右扩展，生成M4,M6
				*/

				switch (i)
				{
				case 0:
					sudoku_board[i + 5][j] = begin_board[i][j];  // 填充9*9棋盘的[4][0~2]位置
					sudoku_board[i + 4][j + 6] = begin_board[i][j];  // 填充9*9棋盘的[5][6~8]位置
					break;

				case 1:
					sudoku_board[i + 2][j] = begin_board[i][j];  // 填充9*9棋盘的[5][0~2]位置
					sudoku_board[i + 4][j + 6] = begin_board[i][j];  // 填充9*9棋盘的[3][6~8]位置
					break;

				case 2:
					sudoku_board[i + 2][j] = begin_board[i][j];  // 填充9*9棋盘的[3][0~2]位置
					sudoku_board[i + 1][j + 6] = begin_board[i][j];  // 填充9*9棋盘的[4][6~8]位置
					break;
				}

				switch (j)
				{
				case 0:
					sudoku_board[i][j + 4] = begin_board[i][j];  // 填充9*9棋盘的[0~2][4]位置
					sudoku_board[i + 6][j + 5] = begin_board[i][j];  // 填充9*9棋盘的[6~8][5]位置
					break;

				case 1:
					sudoku_board[i][j + 4] = begin_board[i][j];  // 填充9*9棋盘的[0~2][5]位置
					sudoku_board[i + 6][j + 2] = begin_board[i][j];  // 填充9*9棋盘的[6~8][3]位置
					break;

				case 2:
					sudoku_board[i][j + 1] = begin_board[i][j];  // 填充9*9棋盘的[0~2][3]位置
					sudoku_board[i + 6][j + 2] = begin_board[i][j];  // 填充9*9棋盘的[6~8][4]位置
					break;
				}
			}

	}

	else
	{
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++)
			{
				sudoku_board[i + 3][j + 3] = begin_board[i][j];  // 生成9*9棋盘的中心的3*3棋盘(M5)

				 /*
				 以基点棋盘进行行变换，向左，右扩展，生成M4,M6
				 */

				switch (i)
				{
				case 0:
					sudoku_board[i + 4][j] = begin_board[i][j];  // 填充9*9棋盘的[4][0~2]位置
					sudoku_board[i + 5][j + 6] = begin_board[i][j];  // 填充9*9棋盘的[5][6~8]位置
					break;

				case 1:
					sudoku_board[i + 4][j] = begin_board[i][j];  // 填充9*9棋盘的[5][0~2]位置
					sudoku_board[i + 2][j + 6] = begin_board[i][j];  // 填充9*9棋盘的[3][6~8]位置
					break;

				case 2:
					sudoku_board[i + 1][j] = begin_board[i][j];  // 填充9*9棋盘的[3][0~2]位置
					sudoku_board[i + 2][j + 6] = begin_board[i][j];  // 填充9*9棋盘的[4][6~8]位置
					break;
				}

				/*
				以基点棋盘进行列变换，向上，向下扩展，生成M2，M8
				*/

				switch (j)
				{
				case 0:
					sudoku_board[i][j + 4] = begin_board[i][j];  // 填充9*9棋盘的[0~2][4]位置
					sudoku_board[i + 6][j + 5] = begin_board[i][j];  // 填充9*9棋盘的[6~8][5]位置
					break;

				case 1:
					sudoku_board[i][j + 4] = begin_board[i][j];  // 填充9*9棋盘的[0~2][5]位置
					sudoku_board[i + 6][j + 2] = begin_board[i][j];  // 填充9*9棋盘的[6~8][3]位置
					break;

				case 2:
					sudoku_board[i][j + 1] = begin_board[i][j];  // 填充9*9棋盘的[0~2][3]位置
					sudoku_board[i + 6][j + 2] = begin_board[i][j];  // 填充9*9棋盘的[6~8][4]位置
					break;
				}
			}


	}




	/*
	以M2作为基点棋盘进行行变换，向左，向右扩展，生成M1，M3
	*/

	for (int i = 0; i <= 2; i++)
		for (int j = 3; j <= 5; j++)
		{
			switch (i)
			{
			case 0:
				sudoku_board[i + 1][j - 3] = sudoku_board[i][j];  // 填充9*9棋盘的[1][0~2]位置
				sudoku_board[i + 2][j + 3] = sudoku_board[i][j];  // 填充9*9棋盘的[2][6~8]位置
				break;

			case 1:
				sudoku_board[i + 1][j - 3] = sudoku_board[i][j];  // 填充9*9棋盘的[2][0~2]位置
				sudoku_board[i - 1][j + 3] = sudoku_board[i][j];  // 填充9*9棋盘的[0][6~8]位置
				break;

			case 2:
				sudoku_board[i - 2][j - 3] = sudoku_board[i][j];  // 填充9*9棋盘的[0][0~2]位置
				sudoku_board[i - 1][j + 3] = sudoku_board[i][j];  // 填充9*9棋盘的[1][6~8]位置
				break;
			}
		}


	/*
	以M8作为基点棋盘进行行变换，向左，向右扩展，生成M7，M9
	*/

	for (int i = 6; i <= 8; i++)
		for (int j = 3; j <= 5; j++)
		{
			switch (i)
			{
			case 6:
				sudoku_board[i + 1][j - 3] = sudoku_board[i][j];  // 填充9*9棋盘的[7][0~2]位置
				sudoku_board[i + 2][j + 3] = sudoku_board[i][j];  // 填充9*9棋盘的[9][6~8]位置
				break;

			case 7:
				sudoku_board[i + 1][j - 3] = sudoku_board[i][j];  // 填充9*9棋盘的[8][0~2]位置
				sudoku_board[i - 1][j + 3] = sudoku_board[i][j];  // 填充9*9棋盘的[6][6~8]位置
				break;

			case 8:
				sudoku_board[i - 2][j - 3] = sudoku_board[i][j];  // 填充9*9棋盘的[6][0~2]位置
				sudoku_board[i - 1][j + 3] = sudoku_board[i][j];  // 填充9*9棋盘的[9][6~8]位置
				break;
			}
		}

}


void generator::generate_txt(int num, char * str) //将数独棋盘写入文件
{
	generator gene;

	freopen(str, "w", stdout);

	for (int k = 0; k < num; k++)
	{
		int len = 0;
		gene.generate_sudoku();

		for (int i = 0; i < 9; i++)
			for (int j = 0; j < 9; j++)
			{
				res[len++] = sudoku_board[i][j] + '0' ;
				res[len++]=' ';

				if (j == 8) res[len++] =' \n';
			}

		puts(res);
	}

	fclose(stdout);
}