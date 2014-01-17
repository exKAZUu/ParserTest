public class Test {
    private final int VERSION = 1;

    public void grow(int a, int b) {
        ;
        {if(a>b){
            b++;
        }}
        int result;
        int x = 2;
        LABEL:
          result = a + b * a;
        switch(result){
            case 10:
            case 11:
            case 12:
              {}
              result++;
              break;
            default:
              result = 0;
        }
    }
}