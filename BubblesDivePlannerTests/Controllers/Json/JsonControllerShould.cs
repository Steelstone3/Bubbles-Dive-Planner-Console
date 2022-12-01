using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.DiveModels.Types;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.Json
{
    public class JsonControllerShould
    {
        private const string EXPECTED_SERIALISED_JSON_ZHL16 = "[\n  {\n    \"DiveModel\": {\n      \"Name\": \"ZHL16\",\n      \"CompartmentCount\": 16,\n      \"NitrogenHalfTimes\": [\n        4.0,\n        8.0,\n        12.5,\n        18.5,\n        27.0,\n        38.3,\n        54.3,\n        77.0,\n        109.0,\n        146.0,\n        187.0,\n        239.0,\n        305.0,\n        390.0,\n        498.0,\n        635.0\n      ],\n      \"HeliumHalfTimes\": [\n        1.51,\n        3.02,\n        4.72,\n        6.99,\n        10.21,\n        14.48,\n        20.53,\n        29.11,\n        41.2,\n        55.19,\n        70.69,\n        90.34,\n        115.29,\n        147.42,\n        188.24,\n        240.03\n      ],\n      \"AValuesNitrogen\": [\n        1.2559,\n        1.0,\n        0.8618,\n        0.7562,\n        0.6667,\n        0.56,\n        0.4947,\n        0.45,\n        0.4187,\n        0.3798,\n        0.3497,\n        0.3223,\n        0.285,\n        0.2737,\n        0.2523,\n        0.2327\n      ],\n      \"BValuesNitrogen\": [\n        0.505,\n        0.6514,\n        0.7222,\n        0.7825,\n        0.8126,\n        0.8434,\n        0.8693,\n        0.891,\n        0.9092,\n        0.9222,\n        0.9319,\n        0.9403,\n        0.9477,\n        0.9544,\n        0.9602,\n        0.9653\n      ],\n      \"AValuesHelium\": [\n        1.7424,\n        1.383,\n        1.1919,\n        1.0458,\n        0.922,\n        0.8205,\n        0.7305,\n        0.6502,\n        0.595,\n        0.5545,\n        0.5333,\n        0.5189,\n        0.5181,\n        0.5176,\n        0.5172,\n        0.5119\n      ],\n      \"BValuesHelium\": [\n        0.4245,\n        0.5747,\n        0.6527,\n        0.7223,\n        0.7582,\n        0.7957,\n        0.8279,\n        0.8553,\n        0.8757,\n        0.8903,\n        0.8997,\n        0.9073,\n        0.9122,\n        0.9171,\n        0.9217,\n        0.9267\n      ],\n      \"DiveProfile\": {\n        \"NitrogenTissuePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"HeliumTissuePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"TotalTissuePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"MaxSurfacePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"ToleratedAmbientPressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"AValues\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"BValues\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"CompartmentLoads\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"OxygenPressureAtDepth\": 12.5,\n        \"HeliumPressureAtDepth\": 12.5,\n        \"NitrogenPressureAtDepth\": 12.5\n      }\n    },\n    \"Cylinders\": [\n      {\n        \"Name\": \"Air\",\n        \"CylinderVolume\": 12,\n        \"CylinderPressure\": 200,\n        \"InitialPressurisedVolume\": 2400,\n        \"RemainingGas\": 2400,\n        \"UsedGas\": 0,\n        \"SurfaceAirConsumptionRate\": 12,\n        \"GasMixture\": {\n          \"Oxygen\": 21,\n          \"Helium\": 0,\n          \"Nitrogen\": 79\n        }\n      },\n      {\n        \"Name\": \"Air\",\n        \"CylinderVolume\": 12,\n        \"CylinderPressure\": 200,\n        \"InitialPressurisedVolume\": 2400,\n        \"RemainingGas\": 2400,\n        \"UsedGas\": 0,\n        \"SurfaceAirConsumptionRate\": 12,\n        \"GasMixture\": {\n          \"Oxygen\": 21,\n          \"Helium\": 0,\n          \"Nitrogen\": 79\n        }\n      }\n    ],\n    \"DiveStep\": {\n      \"Depth\": 50,\n      \"Time\": 10\n    },\n    \"SelectedCylinder\": {\n      \"Name\": \"Air\",\n      \"CylinderVolume\": 12,\n      \"CylinderPressure\": 200,\n      \"InitialPressurisedVolume\": 2400,\n      \"RemainingGas\": 2400,\n      \"UsedGas\": 0,\n      \"SurfaceAirConsumptionRate\": 12,\n      \"GasMixture\": {\n        \"Oxygen\": 21,\n        \"Helium\": 0,\n        \"Nitrogen\": 79\n      }\n    }\n  },\n  {\n    \"DiveModel\": {\n      \"Name\": \"ZHL16\",\n      \"CompartmentCount\": 16,\n      \"NitrogenHalfTimes\": [\n        4.0,\n        8.0,\n        12.5,\n        18.5,\n        27.0,\n        38.3,\n        54.3,\n        77.0,\n        109.0,\n        146.0,\n        187.0,\n        239.0,\n        305.0,\n        390.0,\n        498.0,\n        635.0\n      ],\n      \"HeliumHalfTimes\": [\n        1.51,\n        3.02,\n        4.72,\n        6.99,\n        10.21,\n        14.48,\n        20.53,\n        29.11,\n        41.2,\n        55.19,\n        70.69,\n        90.34,\n        115.29,\n        147.42,\n        188.24,\n        240.03\n      ],\n      \"AValuesNitrogen\": [\n        1.2559,\n        1.0,\n        0.8618,\n        0.7562,\n        0.6667,\n        0.56,\n        0.4947,\n        0.45,\n        0.4187,\n        0.3798,\n        0.3497,\n        0.3223,\n        0.285,\n        0.2737,\n        0.2523,\n        0.2327\n      ],\n      \"BValuesNitrogen\": [\n        0.505,\n        0.6514,\n        0.7222,\n        0.7825,\n        0.8126,\n        0.8434,\n        0.8693,\n        0.891,\n        0.9092,\n        0.9222,\n        0.9319,\n        0.9403,\n        0.9477,\n        0.9544,\n        0.9602,\n        0.9653\n      ],\n      \"AValuesHelium\": [\n        1.7424,\n        1.383,\n        1.1919,\n        1.0458,\n        0.922,\n        0.8205,\n        0.7305,\n        0.6502,\n        0.595,\n        0.5545,\n        0.5333,\n        0.5189,\n        0.5181,\n        0.5176,\n        0.5172,\n        0.5119\n      ],\n      \"BValuesHelium\": [\n        0.4245,\n        0.5747,\n        0.6527,\n        0.7223,\n        0.7582,\n        0.7957,\n        0.8279,\n        0.8553,\n        0.8757,\n        0.8903,\n        0.8997,\n        0.9073,\n        0.9122,\n        0.9171,\n        0.9217,\n        0.9267\n      ],\n      \"DiveProfile\": {\n        \"NitrogenTissuePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"HeliumTissuePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"TotalTissuePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"MaxSurfacePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"ToleratedAmbientPressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"AValues\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"BValues\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"CompartmentLoads\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"OxygenPressureAtDepth\": 12.5,\n        \"HeliumPressureAtDepth\": 12.5,\n        \"NitrogenPressureAtDepth\": 12.5\n      }\n    },\n    \"Cylinders\": [\n      {\n        \"Name\": \"Air\",\n        \"CylinderVolume\": 12,\n        \"CylinderPressure\": 200,\n        \"InitialPressurisedVolume\": 2400,\n        \"RemainingGas\": 2400,\n        \"UsedGas\": 0,\n        \"SurfaceAirConsumptionRate\": 12,\n        \"GasMixture\": {\n          \"Oxygen\": 21,\n          \"Helium\": 0,\n          \"Nitrogen\": 79\n        }\n      },\n      {\n        \"Name\": \"Air\",\n        \"CylinderVolume\": 12,\n        \"CylinderPressure\": 200,\n        \"InitialPressurisedVolume\": 2400,\n        \"RemainingGas\": 2400,\n        \"UsedGas\": 0,\n        \"SurfaceAirConsumptionRate\": 12,\n        \"GasMixture\": {\n          \"Oxygen\": 21,\n          \"Helium\": 0,\n          \"Nitrogen\": 79\n        }\n      }\n    ],\n    \"DiveStep\": {\n      \"Depth\": 50,\n      \"Time\": 10\n    },\n    \"SelectedCylinder\": {\n      \"Name\": \"Air\",\n      \"CylinderVolume\": 12,\n      \"CylinderPressure\": 200,\n      \"InitialPressurisedVolume\": 2400,\n      \"RemainingGas\": 2400,\n      \"UsedGas\": 0,\n      \"SurfaceAirConsumptionRate\": 12,\n      \"GasMixture\": {\n        \"Oxygen\": 21,\n        \"Helium\": 0,\n        \"Nitrogen\": 79\n      }\n    }\n  }\n]";
        private const string EXPECTED_SERIALISED_JSON_USN = "[\n  {\n    \"DiveModel\": {\n      \"Name\": \"FAKE_USN\",\n      \"CompartmentCount\": 12,\n      \"NitrogenHalfTimes\": [\n        4.0,\n        8.0,\n        12.5,\n        18.5,\n        27.0,\n        38.3,\n        54.3,\n        77.0,\n        109.0,\n        146.0,\n        187.0,\n        239.0\n      ],\n      \"HeliumHalfTimes\": [\n        1.51,\n        3.02,\n        4.72,\n        6.99,\n        10.21,\n        14.48,\n        20.53,\n        29.11,\n        41.2,\n        55.19,\n        70.69,\n        90.34\n      ],\n      \"AValuesNitrogen\": [\n        1.2559,\n        1.0,\n        0.8618,\n        0.7562,\n        0.6667,\n        0.56,\n        0.4947,\n        0.45,\n        0.4187,\n        0.3798,\n        0.3497,\n        0.3223\n      ],\n      \"BValuesNitrogen\": [\n        0.505,\n        0.6514,\n        0.7222,\n        0.7825,\n        0.8126,\n        0.8434,\n        0.8693,\n        0.891,\n        0.9092,\n        0.9222,\n        0.9319,\n        0.9403\n      ],\n      \"AValuesHelium\": [\n        1.7424,\n        1.383,\n        1.1919,\n        1.0458,\n        0.922,\n        0.8205,\n        0.7305,\n        0.6502,\n        0.595,\n        0.5545,\n        0.5333,\n        0.5189\n      ],\n      \"BValuesHelium\": [\n        0.4245,\n        0.5747,\n        0.6527,\n        0.7223,\n        0.7582,\n        0.7957,\n        0.8279,\n        0.8553,\n        0.8757,\n        0.8903,\n        0.8997,\n        0.9073\n      ],\n      \"DiveProfile\": {\n        \"NitrogenTissuePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"HeliumTissuePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"TotalTissuePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"MaxSurfacePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"ToleratedAmbientPressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"AValues\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"BValues\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"CompartmentLoads\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"OxygenPressureAtDepth\": 12.5,\n        \"HeliumPressureAtDepth\": 12.5,\n        \"NitrogenPressureAtDepth\": 12.5\n      }\n    },\n    \"Cylinders\": [\n      {\n        \"Name\": \"Air\",\n        \"CylinderVolume\": 12,\n        \"CylinderPressure\": 200,\n        \"InitialPressurisedVolume\": 2400,\n        \"RemainingGas\": 2400,\n        \"UsedGas\": 0,\n        \"SurfaceAirConsumptionRate\": 12,\n        \"GasMixture\": {\n          \"Oxygen\": 21,\n          \"Helium\": 0,\n          \"Nitrogen\": 79\n        }\n      },\n      {\n        \"Name\": \"Air\",\n        \"CylinderVolume\": 12,\n        \"CylinderPressure\": 200,\n        \"InitialPressurisedVolume\": 2400,\n        \"RemainingGas\": 2400,\n        \"UsedGas\": 0,\n        \"SurfaceAirConsumptionRate\": 12,\n        \"GasMixture\": {\n          \"Oxygen\": 21,\n          \"Helium\": 0,\n          \"Nitrogen\": 79\n        }\n      }\n    ],\n    \"DiveStep\": {\n      \"Depth\": 50,\n      \"Time\": 10\n    },\n    \"SelectedCylinder\": {\n      \"Name\": \"Air\",\n      \"CylinderVolume\": 12,\n      \"CylinderPressure\": 200,\n      \"InitialPressurisedVolume\": 2400,\n      \"RemainingGas\": 2400,\n      \"UsedGas\": 0,\n      \"SurfaceAirConsumptionRate\": 12,\n      \"GasMixture\": {\n        \"Oxygen\": 21,\n        \"Helium\": 0,\n        \"Nitrogen\": 79\n      }\n    }\n  },\n  {\n    \"DiveModel\": {\n      \"Name\": \"FAKE_USN\",\n      \"CompartmentCount\": 12,\n      \"NitrogenHalfTimes\": [\n        4.0,\n        8.0,\n        12.5,\n        18.5,\n        27.0,\n        38.3,\n        54.3,\n        77.0,\n        109.0,\n        146.0,\n        187.0,\n        239.0\n      ],\n      \"HeliumHalfTimes\": [\n        1.51,\n        3.02,\n        4.72,\n        6.99,\n        10.21,\n        14.48,\n        20.53,\n        29.11,\n        41.2,\n        55.19,\n        70.69,\n        90.34\n      ],\n      \"AValuesNitrogen\": [\n        1.2559,\n        1.0,\n        0.8618,\n        0.7562,\n        0.6667,\n        0.56,\n        0.4947,\n        0.45,\n        0.4187,\n        0.3798,\n        0.3497,\n        0.3223\n      ],\n      \"BValuesNitrogen\": [\n        0.505,\n        0.6514,\n        0.7222,\n        0.7825,\n        0.8126,\n        0.8434,\n        0.8693,\n        0.891,\n        0.9092,\n        0.9222,\n        0.9319,\n        0.9403\n      ],\n      \"AValuesHelium\": [\n        1.7424,\n        1.383,\n        1.1919,\n        1.0458,\n        0.922,\n        0.8205,\n        0.7305,\n        0.6502,\n        0.595,\n        0.5545,\n        0.5333,\n        0.5189\n      ],\n      \"BValuesHelium\": [\n        0.4245,\n        0.5747,\n        0.6527,\n        0.7223,\n        0.7582,\n        0.7957,\n        0.8279,\n        0.8553,\n        0.8757,\n        0.8903,\n        0.8997,\n        0.9073\n      ],\n      \"DiveProfile\": {\n        \"NitrogenTissuePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"HeliumTissuePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"TotalTissuePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"MaxSurfacePressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"ToleratedAmbientPressures\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"AValues\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"BValues\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"CompartmentLoads\": [\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5,\n          12.5\n        ],\n        \"OxygenPressureAtDepth\": 12.5,\n        \"HeliumPressureAtDepth\": 12.5,\n        \"NitrogenPressureAtDepth\": 12.5\n      }\n    },\n    \"Cylinders\": [\n      {\n        \"Name\": \"Air\",\n        \"CylinderVolume\": 12,\n        \"CylinderPressure\": 200,\n        \"InitialPressurisedVolume\": 2400,\n        \"RemainingGas\": 2400,\n        \"UsedGas\": 0,\n        \"SurfaceAirConsumptionRate\": 12,\n        \"GasMixture\": {\n          \"Oxygen\": 21,\n          \"Helium\": 0,\n          \"Nitrogen\": 79\n        }\n      },\n      {\n        \"Name\": \"Air\",\n        \"CylinderVolume\": 12,\n        \"CylinderPressure\": 200,\n        \"InitialPressurisedVolume\": 2400,\n        \"RemainingGas\": 2400,\n        \"UsedGas\": 0,\n        \"SurfaceAirConsumptionRate\": 12,\n        \"GasMixture\": {\n          \"Oxygen\": 21,\n          \"Helium\": 0,\n          \"Nitrogen\": 79\n        }\n      }\n    ],\n    \"DiveStep\": {\n      \"Depth\": 50,\n      \"Time\": 10\n    },\n    \"SelectedCylinder\": {\n      \"Name\": \"Air\",\n      \"CylinderVolume\": 12,\n      \"CylinderPressure\": 200,\n      \"InitialPressurisedVolume\": 2400,\n      \"RemainingGas\": 2400,\n      \"UsedGas\": 0,\n      \"SurfaceAirConsumptionRate\": 12,\n      \"GasMixture\": {\n        \"Oxygen\": 21,\n        \"Helium\": 0,\n        \"Nitrogen\": 79\n      }\n    }\n  }\n]";
        private readonly double defaultValue = 12.5;
        private readonly List<IDivePlan> divePlans = new();
        private readonly IJsonController jsonController = new JsonController();

        [SkippableFact]
        public void SerialiseZhl16()
        {
            Skip.If(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

            // Given
            SetupZhl16DivePlan();

            // When
            var actualSerialisedJson = jsonController.Serialise(divePlans);

            // Then
            Assert.Equal(EXPECTED_SERIALISED_JSON_ZHL16, actualSerialisedJson);
        }

        [Fact]
        public void DeserialiseZhl16()
        {
            // Given
            SetupZhl16DivePlan();

            // When
            var actualLastDivePlan = jsonController.Deserialise(EXPECTED_SERIALISED_JSON_ZHL16);

            // Then
            var expectedLastDivePlan = divePlans.Last();
            Assert.IsType<Zhl16Buhlmann>(expectedLastDivePlan.DiveModel);
            Assert.Equivalent(expectedLastDivePlan, actualLastDivePlan);
        }

        private void SetupZhl16DivePlan()
        {
            var defaultList = new double[16]
            {
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue
            };
            var diveProfile = new DiveProfile(
                defaultList,
                defaultList,
                defaultList,
                defaultList,
                defaultList,
                defaultList,
                defaultList,
                defaultList,
                defaultValue,
                defaultValue,
                defaultValue
            );
            var diveModel = new Zhl16Buhlmann(diveProfile);

            var divePlan = new DivePlan(diveModel, TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
            divePlans.Add(divePlan);
            divePlans.Add(divePlan);
        }

        [SkippableFact]
        public void SerialiseUsn()
        {
            Skip.If(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

            // Given
            SetupUsnDivePlan();

            // When
            var actualSerialisedJson = jsonController.Serialise(divePlans);

            // Then
            Assert.Equal(EXPECTED_SERIALISED_JSON_USN, actualSerialisedJson);
        }

        [Fact]
        public void DeserialiseUsn()
        {
            // Given
            SetupUsnDivePlan();

            // When
            var actualLastDivePlan = jsonController.Deserialise(EXPECTED_SERIALISED_JSON_USN);

            // Then
            var expectedLastDivePlan = divePlans.Last();
            Assert.IsType<FakeUsnRev6>(expectedLastDivePlan.DiveModel);
            Assert.Equivalent(expectedLastDivePlan, actualLastDivePlan);
        }

        private void SetupUsnDivePlan()
        {
            var defaultList = new double[12]
            {
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
                defaultValue,
            };
            var diveProfile = new DiveProfile(
                defaultList,
                defaultList,
                defaultList,
                defaultList,
                defaultList,
                defaultList,
                defaultList,
                defaultList,
                defaultValue,
                defaultValue,
                defaultValue
            );
            var diveModel = new FakeUsnRev6(diveProfile);

            var divePlan = new DivePlan(diveModel, TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
            divePlans.Add(divePlan);
            divePlans.Add(divePlan);
        }
    }
}