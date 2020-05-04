#include <stdio.h>
#include <string.h>

#include "nn/network.h"
#include "segmentation.h"


int main()
{
	// DO not change those or it will require new training data and training
	EXPECTED_TEXT = "!#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}";
	CHAR_WIDTH = 16;
    CHAR_HEIGHT = 16;

    size_t param[] = {CHAR_WIDTH * CHAR_HEIGHT, 256, 256, strlen(EXPECTED_TEXT)};
    size_t param_size = 4;
    double lRate = 0.005f;

    //char* filePath = "TrainingData/AllCharVariousFont.png";
    char* filePath2 = "TestingData/outhere3.bmp";
    training_file = "nn/training2.txt";
    char* weightFile = "nn/weights3.txt";

    // display_segmentation("TrainingData/AllCharVariousFont.png", 0, 1);
    // display_segmentation("TrainingData/AllCharVariousFont2.png", 0, 1);
    // display_segmentation("TrainingData/arial.png", 0, 1);
    // display_segmentation("TrainingData/helvetica.png", 0, 1);
    // display_segmentation("TrainingData/calibri.png", 0, 1);
    // display_segmentation("TrainingData/cambria.png", 0, 1);
    // display_segmentation("TrainingData/trebuchet.png", 0, 1);
    // display_segmentation("TrainingData/verdana.png", 0, 1);
    // display_segmentation("TrainingData/times.png", 0, 1);
    // display_segmentation("TrainingData/arialSmall.png", 0, 1);
    // display_segmentation("TrainingData/calibriSmall.png", 0, 1);
    // display_segmentation("TrainingData/monospace.png", 0, 1);
    //display_segmentation("TrainingData/Britannic_Bold.png", 0, 1);
    //display_segmentation("TrainingData/Britannic_Bold_G.png", 0, 1);
    //display_segmentation("TrainingData/Britannic_Bold_I.png", 0, 1);
    //display_segmentation("TrainingData/Yi_Baiti.png", 0, 1);
    //display_segmentation("TrainingData/Yi_Baiti_G.png", 0, 1);
    //display_segmentation("TrainingData/Yi_Baiti_I.png", 0, 1);
    //display_segmentation("TrainingData/Verdana_PL.png", 0, 1);
    //display_segmentation("TrainingData/Verdanna_PL_G.png", 0, 1);
    //display_segmentation("TrainingData/Verdana_PL_I.png", 0, 1);
    //display_segmentation("TrainingData/il_sans_nova_L.png", 0, 1);
    //display_segmentation("TrainingData/Gill_sans_nova_L_G.png", 0, 1);
    //display_segmentation("TrainingData/Gill_sans_nova_L_I.png", 0, 1);
    //display_segmentation("TrainingData/fangsong.png", 0, 1);
    //display_segmentation("TrainingData/Fangsong_G.png", 0, 1);
    //display_segmentation("TrainingData/Castellar.png", 0, 1);
    //display_segmentation("TrainingData/Castellar_G.png", 0, 1);
    //display_segmentation("TrainingData/Castellar_I.png", 0, 1);
    //display_segmentation("TrainingData/Grotesque_Light.png", 0, 1);
    //display_segmentation("TrainingData/Grotesque_Light_G.png", 0, 1);
    //display_segmentation("TrainingData/Dubai_Light.png", 0, 1);
    //display_segmentation("TrainingData/Dubai_Light_G.png", 0, 1);
    //display_segmentation("TrainingData/Source_sans_pro_extralight.png", 0, 1);
    //display_segmentation("TrainingData/Source_sans_pro_extra_light_G.png", 0, 1);
    //display_segmentation("TrainingData/Source_sans_pro_extralight_I.png", 0, 1);
    //display_segmentation("TrainingData/Yu_Gothic_light.png", 0, 1);
    //display_segmentation("TrainingData/Yu_gothic_light_G.png", 0, 1);
    //display_segmentation("TrainingData/Yu_gothic_light_I.png", 0, 1);
    //display_segmentation("TrainingData/speak_pro_light_G.png", 0, 1);
    //display_segmentation("TrainingData/The_sherif_hand.png", 0, 1);
    //display_segmentation("TrainingData/The_sherif_hand_G.png", 0, 1);

    char loadWeights = 1;
    if (loadWeights == 1)
    {
    	Load(weightFile);
    } else {
    	Init(param, param_size, lRate, weightFile);
    }

    printf("STARTING TRAINING\n");


    
    // Builds the training data (run once after clearing "nn/training.txt")
    //display_segmentation(filePath, 0, 1);

    // last param is the number of characters in out training data
    for (size_t i = 0; i < 10; i++) {
    	//Train(training_file, 100, EXPECTED_TEXT, strlen(EXPECTED_TEXT) * 20);
    	//display_segmentation(filePath, 0, 0);
    	display_segmentation(filePath2, 0, 0);
    }

    FreeAll();
    return 0;
}