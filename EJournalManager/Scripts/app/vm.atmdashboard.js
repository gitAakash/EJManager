//monitr.dashboardViewModel 
//-- Atms [Name, Location, UptimeStatusCss, IsOffline, IsOnline, IsInSuspendMode, ]
//-- Count
//-- CountStats [StatusName, Count]
//-- SelectedRegion
//-- SelectedStatus
//-- FilteredAtms [Name, Location, UptimeStatusCss]
(function (monitr) {
monitr.vm.atmDashboard = function () {
    var atms = ko.observableArray([]),
        regions = ko.observableArray([]),
        branches = ko.observableArray([]),
        selectedRegion = ko.observable(),
        selectedBranch = ko.observable(),
        selectedStatus = ko.observable(),
        selectedUptimeStatus = ko.observable(),
        filteredAtms = ko.computed(function () {
            // TO DO: filter by branch and region
            //return atms();
            var matchedTerminals = atms();
            if (selectedRegion() !== undefined) {
                matchedTerminals = ko.utils.arrayFilter(matchedTerminals, function (atm) {
                    return atm.regionId() === selectedRegion();
                });
            }
            
            if (selectedBranch() !== undefined) {
                matchedTerminals = ko.utils.arrayFilter(matchedTerminals, function (atm) {
                    return atm.branchId() === selectedBranch();
                });
            }
            
            if (selectedUptimeStatus() !== undefined && selectedUptimeStatus() !== "All Statuses") {
                matchedTerminals = ko.utils.arrayFilter(matchedTerminals, function (atm) {
                    return atm.uptimeStatus() === selectedUptimeStatus();
                });
            }
            
            if (selectedUptimeStatus() !== undefined) {
                matchedTerminals = ko.utils.arrayFilter(matchedTerminals, function (atm) {
                    return atm.uptimeStatus() === 'In Service' ||
                        atm.uptimeStatus() === 'Off-line' ||
                        atm.uptimeStatus() === 'Supervisor' ||
                        atm.uptimeStatus() === 'Low-Cash' ||
                        atm.uptimeStatus() === 'Closed';
                });
            }

            matchedTerminals = matchedTerminals.sort(function (left, right) {
                return left.sortOrder() === right.sortOrder() ? 0 : (left.sortOrder() < right.sortOrder() ? -1 : 1);
            });

            return matchedTerminals;

        }, self),
		
		
        
        atmCount = ko.computed(function () {
            return filteredAtms ? filteredAtms.length : 0;
        }, self),
        loadAtmsCallback = function (json) {
            $.each(json, function(i, atm) {
                atms.push(
                    new monitr.model.Atm()
                        .location(atm.location)
                        .uptimeStatus(atm.uptimeStatus)
                        .terminalId(atm.terminalId)
                        .branchId(atm.branchId)
                        .regionId(atm.regionId)
                        .terminalName(atm.terminalName)
                        .terminalFullName(atm.terminalFullName)
                );
            });
            
            $('#' + 'dashboardPanel').show();
            $('#dashboardAjaxLoading').hide();
        },
        loadAtms = function() {
            $('#' + 'dashboardPanel').hide();
            $('#dashboardAjaxLoading').show();
            monitr.services.terminalDataService.getAtms(monitr.vm.atmDashboard.loadAtmsCallback);
        },
        loadRegionsCallback = function (json) {
            $.each(json, function (i, region) {
                regions.push(
                    new monitr.model.Region()
                        .id(region.id)
                        .name(region.name)
                );
            });
        },
        loadRegions = function () {
            monitr.services.AdminDataService.getAllRegions(monitr.vm.atmDashboard.loadRegionsCallback);
        },
        loadBranchesCallback = function (json) {
             $.each(json, function (i, branch) {
                 branches.push(
                     new monitr.model.Branch()
                         .id(branch.id)
                         .name(branch.name)
                         .regionId(branch.regionId)
                 );
             });
         },
        loadBranches = function () {
            monitr.services.AdminDataService.getRegionBranches(monitr.vm.atmDashboard.loadBranchesCallback, monitr.vm.atmDashboard.selectedRegion());
        },
        addOrUpdateAtms = function (atmList) {
            $.each(atms(), function (index, atm) {
                $.each(atmList, function (index2, updatedAtm) {
                    if (atm.terminalId() == updatedAtm.terminalId) {
                        atm.location(updatedAtm.location);
                        //atm.terminalId(updatedAtm.terminalId);
                        //atm.terminalName(updatedAtm.terminalName);
                        atm.branchId(updatedAtm.branchId);
                        atm.regionId(updatedAtm.regionId);
                        atm.uptimeStatus(updatedAtm.uptimeStatus);
                        console.log('atm update');
                    }
                });
            });
        },
        filteredBranches = ko.computed(function () {
             if (selectedRegion() === undefined) {
                 //alert("null part" + selectedRegion());
                 return branches();
             } else {
                 return ko.utils.arrayFilter(branches(), function (branch) {
                     //alert("real part" + selectedRegion());
                     return branch.regionId() === selectedRegion();
                 });
             }
         }, self),
		 inServiceAtmCount = ko.computed(function () {
			var count = 0;
			$.each(filteredAtms(), function (index, atm) {
				if (atm.isOnline() === true){
					count++;
				}
			});
			//console.log(count);
			return 'Active - ' + count;
		}, self),
		supervisorModeAtmCount = ko.computed(function () {
			var count = 0;
			$.each(filteredAtms(), function (index, atm) {
				if (atm.isInSupervisorMode() === true){
					count++;
				}
			});
			//console.log(count);
			return 'Supervisor - ' + count;
		}, self),
		closedAtmCount = ko.computed(function () {
			var count = 0;
			$.each(filteredAtms(), function (index, atm) {
				if (atm.isClosedMode() === true){
					count++;
				}
			});
			//console.log(count);
			return 'Closed - ' + count;
		}, self),
		lowCashAtmCount = ko.computed(function () {
			var count = 0;
			$.each(filteredAtms(), function (index, atm) {
				if (atm.isLowCashMode() === true){
					count++;
				}
			});
			//console.log(count);
			return 'Low cash - ' + count;
		}, self),
		offlineAtmCount = ko.computed(function () {
			var count = 0;
			$.each(filteredAtms(), function (index, atm) {
				if (atm.isOffline() === true){
					count++;
				}
			});
			//console.log(count);
			return 'Offline - ' + count;
		}, self);
    
    
    //// Whenever the category changes, reset the product selection
    //self.selectedRegion.subscribe(function () {
    //    self.product(undefined);
    //}, self);

    return {        
        atms: atms,
        regions: regions,
        branches: branches,
        filteredAtms: filteredAtms,
        filteredBranches: filteredBranches,
        selectedRegion: selectedRegion,
        selectedStatus: selectedStatus,
        atmCount: atmCount,
        loadAtmsCallback: loadAtmsCallback,
        loadAtms: loadAtms,
        loadRegions: loadRegions,
        loadRegionsCallback: loadRegionsCallback,
        loadBranches: loadBranches,
        loadBranchesCallback: loadBranchesCallback,
        addOrUpdateAtms: addOrUpdateAtms,
        selectedBranch: selectedBranch,
        selectedUptimeStatus: selectedUptimeStatus,
		supervisorModeAtmCount: supervisorModeAtmCount,
		closedAtmCount: closedAtmCount,
		offlineAtmCount: offlineAtmCount,
		lowCashAtmCount: lowCashAtmCount,
		inServiceAtmCount: inServiceAtmCount
    };

}();
}(monitr));


