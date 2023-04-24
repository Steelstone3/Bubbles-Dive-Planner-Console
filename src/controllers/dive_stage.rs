use crate::{
    commands::dive_stages::{
        a_b_values::{calculate_a_value, calculate_b_value},
        ambient_pressures::calculate_ambient_pressure,
        compartment_loads::calculate_compartment_load,
        max_surface_pressures::calculate_max_surface_pressure,
        tissue_pressures::{
            calculate_tissue_pressure_helium, calculate_tissue_pressure_nitrogen,
            calculate_tissue_pressure_total,
        },
        tolerated_ambient_pressures::calculate_tolerated_ambient_pressure,
    },
    models::{
        cylinder::Cylinder, dive_model::DiveModel, dive_profile::DiveProfile, dive_step::DiveStep,
    },
};

pub fn run_dive_profile(
    mut dive_model: DiveModel,
    dive_step: DiveStep,
    cylinder: Cylinder,
) -> DiveProfile {
    dive_model.dive_profile =
        calculate_ambient_pressure(dive_model.dive_profile, dive_step, cylinder.gas_mixture);

    for compartment in 0..dive_model.compartment_count {
        dive_model.dive_profile = update_dive_profile_model(compartment, dive_model, dive_step);
    }

    dive_model.dive_profile
}

fn update_dive_profile_model(
    compartment: usize,
    mut dive_model: DiveModel,
    dive_step: DiveStep,
) -> DiveProfile {
    dive_model.dive_profile.tissue_pressures_nitrogen[compartment] =
        calculate_tissue_pressure_nitrogen(compartment, dive_model, dive_step);
    dive_model.dive_profile.tissue_pressures_helium[compartment] =
        calculate_tissue_pressure_helium(compartment, dive_model, dive_step);
    dive_model.dive_profile.tissue_pressures_total[compartment] =
        calculate_tissue_pressure_total(compartment, dive_model.dive_profile);
    dive_model.dive_profile.a_values[compartment] = calculate_a_value(compartment, dive_model);
    dive_model.dive_profile.b_values[compartment] = calculate_b_value(compartment, dive_model);
    dive_model.dive_profile.tolerated_ambient_pressures[compartment] =
        calculate_tolerated_ambient_pressure(compartment, dive_model.dive_profile);
    dive_model.dive_profile.maximum_surface_pressures[compartment] =
        calculate_max_surface_pressure(compartment, dive_model.dive_profile);
    dive_model.dive_profile.compartment_loads[compartment] =
        calculate_compartment_load(compartment, dive_model.dive_profile);

    dive_model.dive_profile
}

#[cfg(test)]
mod dive_stage_should {
    use super::*;
    use crate::models::{gas_management::GasManagement, gas_mixture::GasMixture};

    #[test]
    fn run_dive_profile() {
        //Arrange
        let zhl16 = DiveModel::create_zhl16_dive_model();
        let dive_step = dive_step_test_fixture();
        let cylinder = cylinder_test_fixture();
        let expected_dive_profile = dive_profile_test_fixture();

        //Act
        let dive_profile = super::run_dive_profile(zhl16, dive_step, cylinder);

        //Assert
        assert_eq!(
            format!("{:.2}", expected_dive_profile.oxygen_at_pressure),
            format!("{:.2}", dive_profile.oxygen_at_pressure)
        );
        assert_eq!(
            format!("{:.2}", expected_dive_profile.helium_at_pressure),
            format!("{:.2}", dive_profile.helium_at_pressure)
        );
        assert_eq!(
            format!("{:.2}", expected_dive_profile.nitrogen_at_pressure),
            format!("{:.2}", dive_profile.nitrogen_at_pressure)
        );

        for compartment in 0..16 {
            assert_eq!(
                format!(
                    "{:.1}",
                    expected_dive_profile.tissue_pressures_nitrogen[compartment]
                ),
                format!("{:.1}", dive_profile.tissue_pressures_nitrogen[compartment])
            );
            assert_eq!(
                format!(
                    "{:.3}",
                    expected_dive_profile.tissue_pressures_helium[compartment]
                ),
                format!("{:.3}", dive_profile.tissue_pressures_helium[compartment])
            );
            assert_eq!(
                format!(
                    "{:.2}",
                    expected_dive_profile.tissue_pressures_total[compartment]
                ),
                format!("{:.2}", dive_profile.tissue_pressures_total[compartment])
            );
            assert_eq!(
                format!("{:.1}", expected_dive_profile.a_values[compartment]),
                format!("{:.1}", dive_profile.a_values[compartment])
            );
            assert_eq!(
                format!("{:.2}", expected_dive_profile.b_values[compartment]),
                format!("{:.2}", dive_profile.b_values[compartment])
            );
            assert_eq!(
                format!(
                    "{:.2}",
                    expected_dive_profile.tolerated_ambient_pressures[compartment]
                ),
                format!(
                    "{:.2}",
                    dive_profile.tolerated_ambient_pressures[compartment]
                )
            );
            assert_eq!(
                format!(
                    "{:.2}",
                    expected_dive_profile.maximum_surface_pressures[compartment]
                ),
                format!("{:.2}", dive_profile.maximum_surface_pressures[compartment])
            );
            assert_eq!(
                format!(
                    "{:.0}",
                    expected_dive_profile.compartment_loads[compartment]
                ),
                format!("{:.0}", dive_profile.compartment_loads[compartment])
            );
        }
    }

    fn dive_step_test_fixture() -> DiveStep {
        DiveStep {
            depth: 50,
            time: 10,
        }
    }

    fn cylinder_test_fixture() -> Cylinder {
        Cylinder {
            cylinder_volume: 12,
            cylinder_pressure: 200,
            initial_pressurised_cylinder_volume: 2400,
            gas_mixture: GasMixture {
                oxygen: 21,
                helium: 10,
                nitrogen: 69,
            },
            gas_management: GasManagement {
                gas_used: 0,
                surface_air_consumption_rate: 12,
            },
        }
    }

    fn dive_profile_test_fixture() -> DiveProfile {
        DiveProfile {
            maximum_surface_pressures: [
                3.35, 2.63, 2.33, 2.1, 1.95, 1.79, 1.68, 1.6, 1.54, 1.48, 1.44, 1.4, 1.35, 1.33, 1.3, 1.28,
            ],
            compartment_loads: [
                124.0, 124.0, 115.0, 105.0, 94.0, 88.0, 81.0, 75.0, 71.0, 69.0, 67.0, 67.0, 67.0, 66.0, 66.0, 66.0,
            ],
            tissue_pressures_nitrogen: [
                3.5, 2.7, 2.2, 1.8, 1.5, 1.3, 1.2, 1.1, 1.0, 0.9, 0.9, 0.9, 0.9, 0.8, 0.8, 0.8,
            ],
            tissue_pressures_helium: [
                0.594, 0.540, 0.462, 0.377, 0.296, 0.228, 0.172, 0.127, 0.093, 0.071, 0.056, 0.044, 0.035, 0.028, 0.022, 0.017,
            ],
            tissue_pressures_total: [
                4.14, 3.27, 2.68, 2.21, 1.84, 1.57, 1.36, 1.21, 1.09, 1.02, 0.97, 0.93, 0.90, 0.88, 0.86, 0.84,
            ],
            tolerated_ambient_pressures: [
                1.39, 1.41, 1.25, 1.09, 0.91, 0.82, 0.72, 0.65, 0.59, 0.57, 0.57, 0.56, 0.57, 0.57, 0.57, 0.58,
            ],
            a_values: [
                1.3, 1.1, 0.9, 0.8, 0.7, 0.6, 0.5, 0.5, 0.4, 0.4, 0.4, 0.3, 0.3, 0.3, 0.3, 0.2,
            ],
            b_values: [
                0.49, 0.64, 0.71, 0.77, 0.80, 0.84, 0.86, 0.89, 0.91, 0.92, 0.93, 0.94, 0.95, 0.95, 0.96, 0.96,
            ],
            oxygen_at_pressure: 1.26,
            helium_at_pressure: 0.600,
            nitrogen_at_pressure: 4.14,
        }
    }
}
