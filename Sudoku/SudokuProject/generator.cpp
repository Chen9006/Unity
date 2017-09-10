#include "generator.h"


int begin_board[3][3];  //数独棋盘中心3*3棋盘(基点棋盘)

int sudoku_board[10][10];
//数独棋盘中心9*9棋盘(9*9棋盘分成9个3*3棋盘填充，从左往右，从上到下记为M1,M2, ... ,M8，M9棋盘)

int flag[10]; //判断随机数是否重复

int  unsigned seed = time(NULL);

int  generator::get_random_number() //获取随机数
{
	srand(seed);
	seed = rand();
	return (rand() % 9 + 1);
}

void generator::generate_sudoku() //构建数独棋盘
{

	memset(begin_board, 0, sizeof(begin_board));
	memset(flag, 0, sizeof(flag));
	memset(sudoku_board, 0, sizeof(sudoku_board));


	/*
	 生成基点3*3棋盘
	*/

	int count = 0;

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
				if (count == 3)
				{
					count = 0;
					break;
				}
			}
		}



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

void generator::print_sudoku() //打印数独棋盘
{

	for (int i = 0; i < 9; i++)
		for (int j = 0; j < 9; j++)
		{
			cout << sudoku_board[i][j] << " ";
			if (j == 8)cout << endl;
		}


	cout << endl;
	cout << endl;

}

void generator::generate_txt(string str) //将数独棋盘写入文件
{
	
	string str1 = str + "/sudoku.txt";
	//cout << str1 << endl;

	ofstream txt(str1, ios::app);
	//txt.open(str1);




	for (int i = 0; i < 9; i++)
		for (int j = 0; j < 9; j++)
		{
			txt << sudoku_board[i][j] << " ";

			if (j == 8) txt << endl;
		}

	txt << endl;
	txt << endl;
	txt.close();
}
