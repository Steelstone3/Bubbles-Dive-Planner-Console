#[cfg(test)]
mod commands_tissue_pressure_should {
    use crate::commands::dive_stages::tissue_pressures::tissue_pressure;
    use crate::factories::zhl16_dive_model::zhl16_dive_model::create_zhl16_dive_model;
    use crate::tests::test_fixtures_dive_plan::test_fixtures_dive_stage::{expected_dive_profile_model, test_fixture_dive_step_default, test_fixture_tissue_pressures_dive_profile_model, test_fixture_tissue_pressures_total_dive_profile_model};

    #[test]
    fn calculate_tissue_pressure_nitrogen() {
        //Arrange
        let mut zhl16 = create_zhl16_dive_model();
        let dive_step = test_fixture_dive_step_default();
        let expected_dive_profile_model = expected_dive_profile_model();
        zhl16.dive_profile = test_fixture_tissue_pressures_dive_profile_model();

        for compartment in 0..16 {
            //Act
            //Assert
            assert_eq!(format!("{:.3}", expected_dive_profile_model.tissue_pressures_nitrogen[compartment]), format!("{:.3}", tissue_pressure::calculate_tissue_pressure_nitrogen(compartment, zhl16, dive_step)));
        }
    }

    #[test]
    fn calculate_tissue_pressure_helium() {
        //Arrange
        let mut zhl16 = create_zhl16_dive_model();
        let dive_step = test_fixture_dive_step_default();
        let expected_dive_profile_model = expected_dive_profile_model();
        zhl16.dive_profile = test_fixture_tissue_pressures_dive_profile_model();

        for compartment in 0..16 {
            //Act
            //Assert
            assert_eq!(format!("{:.3}", expected_dive_profile_model.tissue_pressures_helium[compartment]), format!("{:.3}", tissue_pressure::calculate_tissue_pressure_helium(compartment, zhl16, dive_step)));
        }
    }

    #[test]
    fn calculate_tissue_pressure_total() {
        //Arrange
        let expected_dive_profile_model = expected_dive_profile_model();
        let actual_dive_profile = test_fixture_tissue_pressures_total_dive_profile_model();

        for compartment in 0..16 {
            //Act
            //Assert
            assert_eq!(format!("{:.3}", expected_dive_profile_model.tissue_pressures_total[compartment]), format!("{:.3}", tissue_pressure::calculate_tissue_pressure_total(compartment, actual_dive_profile)));
        }
    }
}