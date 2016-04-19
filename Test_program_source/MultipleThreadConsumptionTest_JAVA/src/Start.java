import java.io.IOException;
import java.util.concurrent.TimeUnit;

public class Start {

	public static int nbThreads;
	public static int sizeTable;
	public static int nbRepeatComputings;

	public static int minmax = 0; // si 0: inactif, sinon envoie valeur a thread
									// principal toute les # valeurs calculées
	public static int max = Integer.MIN_VALUE;
	public static int min = Integer.MAX_VALUE;

	public static int done;
	public static long startTime;
	public static long endTime;

	/**
	 * Main
	 * 
	 * @param args
	 */
	public static void main(String[] args) {

		sizeTable = Integer.parseInt(args[0]);
		nbThreads = Integer.parseInt(args[1]);
		nbRepeatComputings = Integer.parseInt(args[2]);
		minmax = Integer.parseInt(args[3]);

		System.out.println("" + "Lancement des calculs en matrice (" + sizeTable + "x" + sizeTable + ")" + " sur "
				+ nbThreads + " threads..." + " avec un appel de conccurrence tous les " + minmax + " oppération(s)");

		System.out.println("Appuyez sur enter pour lancer le traitement");

		try {
			System.in.read();
		} catch (IOException e) {
			e.printStackTrace();
		}

		startTime = System.currentTimeMillis();

		for (int i = 0; i < nbThreads; i++) {
			final int ni = new Integer(i + 1);
			new Thread(new Runnable() {
				public void run() {
					new MatComputer(ni, sizeTable, nbRepeatComputings, minmax).compute();
				}
			}).start();
		}
	}

	public synchronized static void matComputedDone() {
		done++;

		System.out.println("# threads ok : " + done);

		if (done == nbThreads) {
			endTime = System.currentTimeMillis();
			if (minmax > 0) {
				System.out.println("Fin des calculs");
				System.out.println("Min: " + min + ", Max: " + max);
			}
			double timeSeconds = TimeUnit.MILLISECONDS.toSeconds(endTime - startTime);
			System.out.println("Duree des calculs: " + (endTime - startTime) + " " + timeSeconds);
		}
	}

	public synchronized static void minMax(int number) {
		if (number > max) {
			max = number;
		}
		if (number < min) {
			min = number;
		}
	}

}
