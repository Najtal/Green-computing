import java.util.Random;

public class MatComputer {
	
	private int[][] matrix;
	private int threadNumber;
	private int sizeTable;
	private int nbRepeat;
	private int minmax;
	
	public MatComputer(int threadNumber, int sizeTable, int nbRepeat, int minmax) {
		this.threadNumber = threadNumber;
		this.sizeTable = sizeTable;
		this.nbRepeat = nbRepeat;
		this.minmax = minmax;
	}
	
	public void compute() {

		System.out.println("Start Thread: " + threadNumber);
		
		// On initialise la matrice
		matrix = new int[sizeTable][sizeTable];
		
		Random r = new Random();
		
		// on remplit la matrice de valeurs aléatoires
		for (int i = 0; i < sizeTable-1; i++) {
			for (int j = 0; j < sizeTable-1; j++) {
				matrix[i][j] = r.nextInt(10);
			}
		}
		
		// On process un bete calcul dans la matrice
		int sumMatrix = 0;
		int minmaxCounter = 0;
		boolean plus = true;
		
		for (int c = 0; c < nbRepeat; c++) {
			for (int i = 0; i < sizeTable-1; i++) {
				for (int k = 0; k < sizeTable-1; k++) {
					sumMatrix = ((plus) ? sumMatrix + matrix[i][k] : sumMatrix - matrix[i][k]);
					plus = !plus;
					
					if (minmax > 0) {
						minmaxCounter++;
						if (minmaxCounter%minmax == 0) {
							Start.minMax(sumMatrix);
						}
					}	
				}
			}	
		}
		
		Start.matComputedDone();
	}

}
